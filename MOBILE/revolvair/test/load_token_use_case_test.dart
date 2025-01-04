import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository.dart';
import 'package:resolvair/domain/repositories/token_manager_impl.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/load_token_use_case.dart';
import 'package:test/test.dart';
import 'package:mocktail/mocktail.dart';

class MockSecureStorage extends Mock implements SecureStorageRepository {}
class MockTokenManager extends Mock implements TokenManagerImpl {}
class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late LoadTokenUseCase loadTokenUseCase;
  late MockSecureStorage mockSecureStorage;
  late TokenManagerInterface tokenManager;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    
    tokenManager = MockTokenManager();
    mockSecureStorage = MockSecureStorage();
    tokenManager.setToken("token");
    loadTokenUseCase = LoadTokenUseCase(tokenManager: tokenManager, secureStorage: mockSecureStorage);
  });

  test('load_token_successfuly', () async {
    when(() => mockSecureStorage.retriveToken()).thenAnswer((_) async => const Success("token"));
    var result = await loadTokenUseCase.execute();

    expect(result.tryGetSuccess(), unit);
  });

  test('try_to_load_token_but_return_error_when_retrive_from_secure_storage', () async {
    when(() => mockSecureStorage.retriveToken()).thenAnswer((_) async => Error(ResponseFailure()));
    var result = await loadTokenUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('try_load_token_into_manager_but_token_is_string_empty', () async {
    when(() => mockSecureStorage.retriveToken()).thenAnswer((_) async => const Success(""));
    var result = await loadTokenUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('try_load_token_into_manager_but_token_is_null', () async {
    when(() => mockSecureStorage.retriveToken()).thenAnswer((_) async => const Success(null));
    var result = await loadTokenUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
