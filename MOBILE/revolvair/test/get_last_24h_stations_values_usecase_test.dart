import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_station_value_24H_usecase.dart';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';

import './fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetStationValue24HUsecase getStationValue24HUseCase;
  late RevolvairService apiRest;
  late http.Client mockHttpClient;
  late TokenManagerInterface mockTokenManager;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    registerFallbackValue(FakeUri());
    mockHttpClient = MockHttpClient();
    mockTokenManager = MockTokenManager();
    apiRest = RevolvairService(
        httpClient: mockHttpClient, tokenManager: mockTokenManager);
    getStationValue24HUseCase = GetStationValue24HUsecase(apiRest: apiRest);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('should return a StationValue list when execute is called successfully',
      () async {
    final fakeStationValuesData =
        Fixture.createStationsValues(file: 'stationsValues.json');
    var uri = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/measures/pm25/last");

    when(() => mockHttpClient.get(uri)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'stationsValues.json', code: 200));

    var result = await getStationValue24HUseCase.execute();

    expect(result.tryGetSuccess(), fakeStationValuesData);
  });

  test('Should throw NoServiceException when status code isnt 200', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 418));
    var result = await getStationValue24HUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
