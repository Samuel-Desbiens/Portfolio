import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:resolvair/data/station_detail_service_impl.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_station_comments_usecase.dart';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';

import './fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetStationCommentsUseCase getStationCommentsUseCase;
  late StationDetailsService apiRest;
  late http.Client mockHttpClient;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    registerFallbackValue(FakeUri());
    mockHttpClient = MockHttpClient();
    apiRest = StationDetailsService(httpClient: mockHttpClient);
    getStationCommentsUseCase = GetStationCommentsUseCase(detailApi: apiRest);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('should return a Comments list when execute is called successfully',
      () async {
    final fakeStationValuesData = Fixture.createComments(file: 'comments.json');
    var uri = Uri.parse(
        "https://staging.revolvair.org/api/stations/levis-parc-georges-maranda-frm/comments");

    when(() => mockHttpClient.get(uri)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'comments.json', code: 200));

    var result = await getStationCommentsUseCase.execute(
        slug: 'levis-parc-georges-maranda-frm');

    expect(result.tryGetSuccess(), fakeStationValuesData);
  });

  test('Should throw NoServiceException when status code isnt 200', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'comments.json', code: 418));
    var result = await getStationCommentsUseCase.execute(
        slug: 'levis-parc-georges-maranda-frm');

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
