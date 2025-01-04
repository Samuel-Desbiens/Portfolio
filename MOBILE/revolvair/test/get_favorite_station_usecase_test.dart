import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_favorite_station_use_case.dart';
import 'package:test/test.dart';

import 'fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetFavoriteStationUseCase getFavoriteStationUseCase;
  late RevolvairService apiRest;
  late http.Client mockHttpClient;
  late TokenManagerInterface mockTokenManager;
  late Uri uri;

  setUpAll(() async {
    mockHttpClient = MockHttpClient();
    mockTokenManager = MockTokenManager();
    apiRest = RevolvairService(
        httpClient: mockHttpClient, tokenManager: mockTokenManager);
    getFavoriteStationUseCase = GetFavoriteStationUseCase(stationApi: apiRest);
    uri = Uri.parse(
        'https://staging.revolvair.org/api/revolvair/stations/affections');
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test(
      'When the user is connected, it should retrieve favorite data with Bearer token',
      () async {
    const fakeToken =
        "eyJ0e8t4i0Yc0AfwEEyKzDM_NnrhDVqZevmaRMk6cQADfrgx1Rl4Daekc9weky_iEzd_5_sVaUbmxck";
    final fakeFavoriteData =
        Fixture.createFavorite(file: 'favoritestation.json');
    final headers = {
      'Authorization': 'Bearer $fakeToken',
    };

    when(() => mockTokenManager.getToken()).thenReturn(fakeToken);
    when(() => mockHttpClient.get(uri, headers: headers)).thenAnswer((_) =>
        Fixture.createHttpResponse(file: 'favoritestation.json', code: 200));

    var result = await getFavoriteStationUseCase.execute();

    expect(result.tryGetSuccess(), fakeFavoriteData);
  });
}
