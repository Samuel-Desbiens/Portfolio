import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';

import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/post_comment_use_case.dart';
import 'package:test/test.dart';
import 'package:resolvair/domain/failures/response_failure.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late PostCommentUseCase postComment;
  late RevolvairService revolvairService;
  late http.Client mockHttpClient;
  late TokenManagerInterface mockTokenManager;

  setUpAll(() async {
    mockHttpClient = MockHttpClient();
    mockTokenManager = MockTokenManager();
    revolvairService = RevolvairService(
        httpClient: mockHttpClient, tokenManager: mockTokenManager);
    postComment = PostCommentUseCase(stationApi: revolvairService);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('On valid comment, should return string.', () async {
    const fakeComment = "Commentaire ajouté";
    const apiUrl =
        'https://staging.revolvair.org/api/stations/levis-parc-georges-maranda-frm/comments';

    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer((_) async => http.Response('connexion reussie', 201));

    when(() => mockTokenManager.getToken()).thenAnswer((_) => "fakeToken");

    var result =
        await postComment.execute("test3", "levis-parc-georges-maranda-frm");

    expect(result.tryGetSuccess(), fakeComment);
  });

  test('On other code than 201, should return an error.', () async {
    const apiUrl = 'https://staging.revolvair.org/api/stations/test/comments';

    when(() => mockHttpClient.post(Uri.parse(apiUrl),
            headers: any(named: 'headers'), body: any(named: 'body')))
        .thenAnswer((_) async => http.Response('error', 401));

    when(() => mockTokenManager.getToken()).thenAnswer((_) => "fakeToken");

    var result = await postComment.execute("test3", "test");

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
