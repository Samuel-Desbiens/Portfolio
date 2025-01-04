import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_station_use_case.dart';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';

import './fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetStationUseCase getStationUseCase;
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
    getStationUseCase = GetStationUseCase(apiRest: apiRest);
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('should return a Station list when execute is called successfully',
      () async {
    final fakeStationData = Fixture.createStations(file: 'stations.json');
    var uri = Uri.parse("https://staging.revolvair.org/api/revolvair/stations");

    when(() => mockHttpClient.get(uri)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'stations.json', code: 200));

    var result = await getStationUseCase.execute();

    expect(result.tryGetSuccess(), fakeStationData);
  });

  test('Should throw NoServiceException when status code is 404', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 404));
    var result = await getStationUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('Should throw NoServiceException when status code is 500', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 500));
    var result = await getStationUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('Should throw NoServiceException when status code is 422', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 422));
    var result = await getStationUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('Should throw NoServiceException when status code is 401', () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 401));
    var result = await getStationUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });

  test('Should throw ResponseFailure when passed wrong organisation string',
      () async {
    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 404));
    var result = await getStationUseCase.execute();

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
