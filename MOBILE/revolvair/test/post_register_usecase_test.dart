import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';
import 'package:resolvair/data/api_auth_impl.dart';
import 'package:resolvair/domain/failures/auth_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/post_register_use_case.dart';
import 'package:test/test.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late PostRegisterUseCase postRegister;
  late AuthService apiService;
  late http.Client mockHttpClient;
  late TokenManagerInterface mockTokenManager;

  setUpAll(() async {
    mockHttpClient = MockHttpClient();
    mockTokenManager = MockTokenManager();
    apiService =
        AuthService(httpClient: mockHttpClient, tokenManager: mockTokenManager);
    postRegister = PostRegisterUseCase(apiService: apiService);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('On valid credential, should return the new user Token.', () async {
    const fakeToken = "fakefakefakefakefakefakefakefake";
    const apiUrl = 'https://staging.revolvair.org/api/register';

    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer((_) async => http.Response('{"token": "$fakeToken"}', 201));

    var result = await postRegister.execute("non", "fake@fake.com", "fakeF@k3");

    expect(result.tryGetSuccess(), fakeToken);
  });

  test('On other code than 200, should return an error.', () async {
    const apiUrl = 'https://staging.revolvair.org/api/register';
    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer(
            (_) async => http.Response('{"message": "Unauthorized"}', 401));

    var result =
        await postRegister.execute("non", "fake@fake.fake", "fakeF@k3");

    expect(result.tryGetError(), isA<AuthError>());
  });

  test('On invalid credentials, should not make the call.', () async {
    const String token = 'tudevraitpasavoirca';
    const apiUrl = 'https://staging.revolvair.org/api/register';
    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer((_) async => http.Response('{"token": $token}', 200));

    var result = await postRegister.execute("non", "fake@fake.fake", "no");

    expect(result.tryGetError(), isA<AuthError>());
  });
}
