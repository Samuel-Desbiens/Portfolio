import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/api_auth_impl.dart';
import 'package:resolvair/domain/failures/auth_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/logout_usecase.dart';
import 'package:resolvair/domain/usecases/login_usecase.dart';
import 'package:test/test.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetAuthUseCase getAuthUseCase;
  late GetAuthLogoutUseCase getAuthLogoutUseCase;
  late AuthService authService;
  late http.Client mockHttpClient;
  late TokenManagerInterface mockTokenManager;

  setUpAll(() async {
    mockHttpClient = MockHttpClient();
    mockTokenManager = MockTokenManager();
    authService =
        AuthService(httpClient: mockHttpClient, tokenManager: mockTokenManager);
    getAuthUseCase = GetAuthUseCase(
        authService: authService, tokenManager: mockTokenManager);
    getAuthLogoutUseCase = GetAuthLogoutUseCase(authService: authService);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('On valid credential, should return the user Token.', () async {
    const fakeToken =
        "eyJ0e8t4i0Yc0AfwEEyKzDM_NnrhDVqZevmaRMk6cQADfrgx1Rl4Daekc9weky_iEzd_5_sVaUbmxck";
    const apiUrl = 'https://staging.revolvair.org/api/login';

    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer((_) async => http.Response('{"token": "$fakeToken"}', 200));

    var result =
        await getAuthUseCase.execute("testtest@hotmail.com", "testtest");

    expect(result.tryGetSuccess(), fakeToken);
  });

  test('On invalid credential, should return an error.', () async {
    const apiUrl = 'https://staging.revolvair.org/api/login';
    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer(
            (_) async => http.Response('{"message": "Unauthorized"}', 401));

    var result =
        await getAuthUseCase.execute("testtest@hotmail.com", "testtest");

    expect(result.tryGetError(), isA<LoginError>());
  });

  test('Successful logout', () async {
    const fakeToken =
        "eyJ0e8t4i0Yc0AfwEEyKzDM_NnrhDVqZevmaRMk6cQADfrgx1Rl4Daekc9weky_iEzd_5_sVaUbmxck";
    const apiUrl = 'https://staging.revolvair.org/api/logout';

    when(() => mockTokenManager.getToken()).thenReturn(fakeToken);

    when(() => mockHttpClient.post(
          Uri.parse(apiUrl),
          headers: any(named: 'headers'),
        )).thenAnswer((_) async => http.Response(
          '{"message": "La déconnexion a été effectuée avec succès."}',
          200,
        ));

    final result = await getAuthLogoutUseCase.execute();
    expect(result, isA<Success>());
  });
}
