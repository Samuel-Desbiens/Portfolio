import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:resolvair/data/station_detail_service_impl.dart';
import 'package:resolvair/domain/entities/stationStats.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/usecases/get_statistiques_usecase.dart';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';

import './fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetStatistiqueUseCase getStatistiqueUseCase;
  late StationDetailsService apiRest;
  late http.Client mockHttpClient;
  late StationStats stationStats;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    registerFallbackValue(FakeUri());
    mockHttpClient = MockHttpClient();
    apiRest = StationDetailsService(httpClient: mockHttpClient);
    getStatistiqueUseCase = GetStatistiqueUseCase(apiRest: apiRest);
    stationStats = Fixture.createStationStats(file: "stationStats.json");
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('execute_returnStationStats_code200', () async {
    var uri0 = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25/last");
    var uri1 = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25_raw/average/24h");
    var uri2 = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25/24h/peaks");
    var uri3 = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25/week/peaks");
    when(() => mockHttpClient.get(uri0)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'lastValue.json', code: 200));
    when(() => mockHttpClient.get(uri1)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'values.json', code: 200));
    when(() => mockHttpClient.get(uri2)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'minmax.json', code: 200));
    when(() => mockHttpClient.get(uri3)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'minmax.json', code: 200));

    var result = await getStatistiqueUseCase.execute(
        slug: 'levis-parc-georges-maranda-frm');

    expect(result.tryGetSuccess(), stationStats);
  });

  test('execute_returnErrorBecauseOfOneURL', () async {
    var uri0 = Uri.parse("https://staging.revolvair.org/api/revolvair/stations/slugInvalide/measures/pm25/last");
    when(() => mockHttpClient.get(uri0)).thenAnswer((_) async =>
      Fixture.createHttpResponse(file: 'lastValue.json', code: 404));
    var result = await getStatistiqueUseCase.execute(slug: 'slugInvalide');

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('execute_returnErrorBecauseOfTwoURL', () async {
      final httpResponse  = http.Response('SERVER_ERROR', 500);
      var uri0 = Uri.parse("https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25/last");
      var uri1 = Uri.parse("https://staging.revolvair.org/api/revolvair/stations/levis-parc-georges-maranda-frm/measures/pm25_raw/average/24h");
      when(() => mockHttpClient.get(uri0)).thenAnswer((_) async => httpResponse);
      when(() => mockHttpClient.get(uri1)).thenAnswer((_) async => httpResponse);
      var result = await getStatistiqueUseCase.execute(slug: 'levis-parc-georges-maranda-frm');
      expect(result.tryGetError(), isA<Failure>());
  });
}
