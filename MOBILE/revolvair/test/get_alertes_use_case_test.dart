import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:resolvair/data/station_detail_service_impl.dart';
import 'package:resolvair/domain/entities/alert.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/usecases/get_alertes_usecase.dart';
import 'package:test/test.dart';
import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';

import './fixtures/fixtures.dart';

class MockHttpClient extends Mock implements http.Client {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late GetAlertesUseCase getAlertesUseCase;
  late StationDetailsService apiRest;
  late http.Client mockHttpClient;
  late Uri uri;

  setUpAll(() async {
    await dotenv.load(fileName: '.env');
    registerFallbackValue(FakeUri());
    mockHttpClient = MockHttpClient();
    apiRest = StationDetailsService(httpClient: mockHttpClient);
    getAlertesUseCase = GetAlertesUseCase(apiRest: apiRest);
    uri = Uri.parse(
        "https://staging.revolvair.org/api/revolvair/stations/SlugValide/alerts/details?page=1");
  });

  tearDown(() async {
    mockHttpClient.close();
  });

  test('returnExacteNumberOfAlerts', () async {
    when(() => mockHttpClient.get(uri)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'alertepage1.json', code: 200));
    var result = await getAlertesUseCase.getAlertesData(
        slug: "SlugValide", receivedPageNumber: 1);
    int received = result.tryGetSuccess()?[1][6];
    expect(received, 13);
  });

  test('returnValideAlertData', () async {
    final fakeAlerstData = Fixture.createAlertStats(file: 'fakeAlerte.json');

    when(() => mockHttpClient.get(uri)).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'alertepage1.json', code: 200));
    var result = await getAlertesUseCase.getAlertesData(
        slug: "SlugValide", receivedPageNumber: 1);
    Alertes received = result.tryGetSuccess()?[0][0];

    expect(received, fakeAlerstData);
  });

  test('execute_return404', () async {
    final httpResponse = http.Response('NOT_FOUND', 404);
    when(() => mockHttpClient.get(uri)).thenAnswer((_) async => httpResponse);
    var result = await getAlertesUseCase.getAlertesData(
        slug: 'slugInvalide', receivedPageNumber: 1);
    expect(result.tryGetError(), isA<Failure>());
  });

  test('execute_return500', () async {
    final httpResponse = http.Response('SERVER_ERROR', 500);
    when(() => mockHttpClient.get(uri)).thenAnswer((_) async => httpResponse);
    var result = await getAlertesUseCase.getAlertesData(
        slug: 'levis-parc-georges-maranda-frm', receivedPageNumber: 1);
    expect(result.tryGetError(), isA<Failure>());
  });
}
