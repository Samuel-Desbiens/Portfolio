import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository.dart';
import 'package:resolvair/domain/repositories/token_manager_impl.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/store_token_use_case.dart';
import 'package:test/test.dart';
import 'package:mocktail/mocktail.dart';

class MockSecureStorage extends Mock implements SecureStorageRepository {}
class MockTokenManager extends Mock implements TokenManagerImpl {}
class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late StoreTokenUseCase storeTokenUseCase;
  late MockSecureStorage mockSecureStorage;
  late TokenManagerInterface tokenManager;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    
    tokenManager = MockTokenManager();
    mockSecureStorage = MockSecureStorage();
    storeTokenUseCase = StoreTokenUseCase(tokenManager: tokenManager, secureStorage: mockSecureStorage);
  });

  test('store_token_successfuly', () async {
    when(() => tokenManager.getToken()).thenAnswer((_) => "token");
    when(() => mockSecureStorage.storeToken("token")).thenAnswer((_) async => const Success(unit));
    var result = await storeTokenUseCase.execute();

    expect(result.tryGetSuccess(), unit);
  });

  test('try_store_token_but_destroy_successfuly', () async {
    when(() => tokenManager.getToken()).thenAnswer((_) => "");
    when(() => mockSecureStorage.deleteToken()).thenAnswer((_) async => const Success(unit));
    var result = await storeTokenUseCase.execute();

    expect(result.tryGetSuccess(), unit);
  });

  test('try_to_store_token_but_return_error_when_store_in_secure_storage', () async {
    when(() => tokenManager.getToken()).thenAnswer((_) => "token");
    when(() => mockSecureStorage.storeToken("token")).thenAnswer((_) async => Error(ResponseFailure()));
    var result = await storeTokenUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('try_to_store_token_but_delete_because_stringEmpty_return_error_when_destroy_in_secure_storage', () async {
    when(() => tokenManager.getToken()).thenAnswer((_) => "");
    when(() => mockSecureStorage.deleteToken()).thenAnswer((_) async => Error(ResponseFailure()));
    var result = await storeTokenUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

}
