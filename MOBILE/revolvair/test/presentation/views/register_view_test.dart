import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_test/flutter_test.dart';
import 'package:geolocator/geolocator.dart';
import 'package:get_it/get_it.dart';
import 'package:mocktail/mocktail.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/wrapper/geolocator_wrapper.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/repositories/token_manager_impl.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_air_quality_use_case.dart';
import 'package:resolvair/domain/usecases/get_location_use_case.dart';
import 'package:resolvair/domain/usecases/get_station_use_case.dart';
import 'package:resolvair/domain/usecases/get_station_value_24H_usecase.dart';
import 'package:resolvair/domain/usecases/load_token_use_case.dart';
import 'package:resolvair/domain/usecases/logout_usecase.dart';
import 'package:resolvair/domain/usecases/post_register_use_case.dart';
import 'package:resolvair/domain/usecases/store_token_use_case.dart';
import 'package:resolvair/presentation/views/home/home_view.dart';
import 'package:stacked_services/stacked_services.dart';
import '../../fixtures/fixtures.dart';
import 'package:multiple_result/src/result.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/register/register_view.dart';
import 'package:easy_localization/easy_localization.dart';

class MockDialogService extends Mock implements DialogService {}

class MockNavigationService extends Mock implements NavigationService {}

class MockPostRegisterUseCase extends Mock implements PostRegisterUseCase {}

class MockGetStationUseCase extends Mock implements GetStationUseCase {}

class MockGetLocationUseCase extends Mock implements GetLocationUseCase {}

class MockLoadTokenUseCase extends Mock implements LoadTokenUseCase {}

class MockStoreTokenUseCase extends Mock implements StoreTokenUseCase {}

class MockGetAirQualityUseCase extends Mock implements GetAirQualityUseCase {}

class MockGetStationValue24HUsecase extends Mock
    implements GetStationValue24HUsecase {}

class MockGetTokenManagerInterface extends Mock implements TokenManagerImpl {}

class MockGetAuthLogoutUseCase extends Mock implements GetAuthLogoutUseCase {}

class MockGeoLocatorWrapper extends Mock implements GeoLocatorWrapper {}

class MockHttpClient extends Mock implements http.Client {}

class FakeUri extends Fake implements Uri {}

class MockNavigatorObserver extends Mock implements NavigatorObserver {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

final locator = GetIt.instance;

void main() {
  late MockDialogService mockDialogService;
  late MockPostRegisterUseCase mockPostRegisterUseCase;
  late MockNavigationService mockNavigationService;
  late TokenManagerInterface mockTokenManager;
  late MockGetStationUseCase mockGetStationUseCase;
  late MockGetLocationUseCase mockGetLocationUseCase;
  late MockLoadTokenUseCase mockLoadTokenUseCase;
  late MockStoreTokenUseCase mockStoreTokenUseCase;
  late MockGetAirQualityUseCase mockGetAirQualityUseCase;
  late MockGetStationValue24HUsecase mockGetStationValue24HUsecase;
  late MockGetAuthLogoutUseCase mockGetAuthLogoutUseCase;
  late MockGeoLocatorWrapper mockGeoLocatorWrapper;
  late http.Client mockHttpClient;

  setUp(() {
    locator.reset();
    mockDialogService = MockDialogService();
    mockTokenManager = MockTokenManager();
    mockPostRegisterUseCase = MockPostRegisterUseCase();
    mockNavigationService = MockNavigationService();
    mockTokenManager = MockTokenManager();
    locator.registerLazySingleton<DialogService>(() => mockDialogService);
    locator.registerLazySingleton<PostRegisterUseCase>(
        () => mockPostRegisterUseCase);
    locator
        .registerLazySingleton<NavigationService>(() => mockNavigationService);

    locator
        .registerLazySingleton<TokenManagerInterface>(() => mockTokenManager);
    locator.reset();
    registerFallbackValue(FakeUri());

    mockGetStationUseCase = MockGetStationUseCase();
    mockGetLocationUseCase = MockGetLocationUseCase();
    mockLoadTokenUseCase = MockLoadTokenUseCase();
    mockStoreTokenUseCase = MockStoreTokenUseCase();
    mockGetAirQualityUseCase = MockGetAirQualityUseCase();
    mockGetStationValue24HUsecase = MockGetStationValue24HUsecase();
    mockGetAuthLogoutUseCase = MockGetAuthLogoutUseCase();
    mockGeoLocatorWrapper = MockGeoLocatorWrapper();
    mockHttpClient = MockHttpClient();

    locator
        .registerLazySingleton<GetStationUseCase>(() => mockGetStationUseCase);
    locator.registerLazySingleton<GetLocationUseCase>(
        () => mockGetLocationUseCase);
    locator.registerLazySingleton<LoadTokenUseCase>(() => mockLoadTokenUseCase);
    locator
        .registerLazySingleton<StoreTokenUseCase>(() => mockStoreTokenUseCase);
    locator.registerLazySingleton<GetAirQualityUseCase>(
        () => mockGetAirQualityUseCase);
    locator.registerLazySingleton<GetStationValue24HUsecase>(
        () => mockGetStationValue24HUsecase);
    locator.registerLazySingleton<GetAuthLogoutUseCase>(
        () => mockGetAuthLogoutUseCase);
    locator
        .registerLazySingleton<GeoLocatorWrapper>(() => mockGeoLocatorWrapper);
  });

  testWidgets('La view renvoie vers home après une inscription ',
      (tester) async {
    var token = "bidon";
    when(() => mockPostRegisterUseCase.execute(
            "testeur", "testtest@hotmail.com", "Testtest1"))
        .thenAnswer((_) async => Result.success(token));

    when(() => mockTokenManager.getToken()).thenAnswer((_) => token);

    await tester.pumpWidget(
      EasyLocalization(
        supportedLocales: const [Locale('fr', 'CA')],
        path: 'assets/translations',
        startLocale: const Locale('fr', 'CA'),
        child: const MaterialApp(
          home: RegisterView(),
        ),
      ),
    );

    await tester.pumpAndSettle();

    final nameTextField =
        find.widgetWithText(TextFormField, LocaleKeys.app_text_your_name.tr());
    final emailTextField =
        find.widgetWithText(TextFormField, LocaleKeys.app_text_hint_email.tr());
    final passwordTextField = find.widgetWithText(
        TextFormField, LocaleKeys.app_text_password_confirm.tr());

    await tester.enterText(nameTextField, "testeur");
    await tester.enterText(emailTextField, "testtest@hotmail.com");
    await tester.enterText(passwordTextField, "Testtest1");
    await tester.tap(find.byType(Checkbox));

    await tester.pumpAndSettle();

    final registerButton = find.text(LocaleKeys.app_text_sinscrire.tr());
    await tester.tap(registerButton);
    await tester.pumpAndSettle();

    expect(find.byType(ElevatedButton), findsOneWidget);

    verify(() => mockNavigationService.popRepeated(2)).called(1);
  });

  testWidgets(
      "Quand j'essaye de m'inscrire et que les champs sont vides, un message d'erreur apparait",
      (WidgetTester tester) async {
    final String expectedNameError =
        LocaleKeys.authentification_empty_name.tr();
    final String expectedMailError =
        LocaleKeys.authentification_empty_email.tr();
    final String expectedPswrdError =
        LocaleKeys.authentification_empty_password.tr();

    await tester.pumpWidget(
      const MaterialApp(
        home: RegisterView(),
      ),
    );

    await tester.pumpAndSettle();

    final box = find.byType(Checkbox);
    await tester.tap(box);
    await tester.pumpAndSettle();

    expect(tester.widget<Checkbox>(box).value, true);

    final btn = find.byType(ElevatedButton);
    await tester.tap(btn);
    await tester.pumpAndSettle();

    final nameError = find.text(expectedNameError);
    final emailError = find.text(expectedMailError);
    final pswrdError = find.text(expectedPswrdError);
    expect(nameError, findsOneWidget);
    expect(emailError, findsOneWidget);
    expect(pswrdError, findsOneWidget);
  });

  testWidgets(
    "Doit afficher un message d'erreur lorsque le compte existe déjà.",
    (tester) async {
      final Result<String, Failure> test;
      test = Result.error(ResponseFailure());
      when(() => mockPostRegisterUseCase.execute(
            "Testing",
            "example@fournisseur.com",
            "Password1",
          )).thenAnswer((_) async => test);
      when(() => mockTokenManager.getToken()).thenReturn("token");
      when(() => mockDialogService.showDialog(
            title: any(named: 'title'),
            description: any(named: 'description'),
          )).thenAnswer((_) async => null);

      await tester.pumpWidget(
        EasyLocalization(
          supportedLocales: const [Locale('fr', 'CA')],
          path: 'assets/translations',
          startLocale: const Locale('fr', 'CA'),
          child: const MaterialApp(
            home: RegisterView(),
          ),
        ),
      );

      await tester.pumpAndSettle();

      final nameTextField = find.widgetWithText(
          TextFormField, LocaleKeys.app_text_your_name.tr());
      final emailTextField = find.widgetWithText(
          TextFormField, LocaleKeys.app_text_hint_email.tr());
      final passwordTextField = find.widgetWithText(
          TextFormField, LocaleKeys.app_text_password_confirm.tr());

      await tester.enterText(nameTextField, "Testing");
      await tester.enterText(emailTextField, "example@fournisseur.com");
      await tester.enterText(passwordTextField, "Password1");

      final checkbox = find.byType(Checkbox);
      await tester.tap(checkbox);
      await tester.pumpAndSettle();

      final registerButton = find.text(LocaleKeys.app_text_sinscrire.tr());
      await tester.tap(registerButton);
      await tester.pumpAndSettle();

      verify(() => mockDialogService.showDialog(
            title: LocaleKeys.app_text_error.tr(),
            description: any(named: 'description'),
          )).called(1);
    },
  );

  testWidgets(
      "Doit afficher \"se connecter\" dans le menu lorsque l'utilisateur est déconnecté",
      (tester) async {
    Result<Position, Failure> position =
        Result.success(Fixture.createExamplePositions());

    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'ranges.json', code: 200));

    when(() => mockGeoLocatorWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(true));
    when(() => mockGeoLocatorWrapper.checkPermission())
        .thenAnswer((_) async => Future.value(LocationPermission.always));

    when(() => mockGetLocationUseCase.execute())
        .thenAnswer((_) async => position);
    when(() => mockGetStationUseCase.execute()).thenAnswer((_) async =>
        Result.success(Fixture.createStations(file: 'stations.json')));
    when(() => mockGetAirQualityUseCase.execute(organisation: "revolvair"))
        .thenAnswer((_) async =>
            Result.success(Fixture.createRanges(file: 'ranges.json')));
    when(() => mockGetStationValue24HUsecase.execute()).thenAnswer((_) async =>
        Result.success(
            Fixture.createStationsValues(file: 'stationsValues.json')));
    when(() => mockTokenManager.getToken()).thenAnswer((_) => "");

    await tester.pumpWidget(
      EasyLocalization(
        supportedLocales: const [Locale('fr', 'CA')],
        path: 'assets/translations',
        startLocale: const Locale('fr', 'CA'),
        child: const MaterialApp(
          home: HomeView(),
        ),
      ),
    );

    await tester.pumpAndSettle();

    var drawer = find.byTooltip("Open navigation menu");
    await tester.tap(drawer);
    await tester.pump();

    final taskTextFinder = find.text(LocaleKeys.app_text_drawer_connexion.tr());
    expect(taskTextFinder, findsOneWidget);
  });

  testWidgets(
      "Doit afficher \"se déconnecter\" dans le menu lorsque l'utilisateur est connecté",
      (WidgetTester tester) async {
    Result<Position, Failure> position =
        Result.success(Fixture.createExamplePositions());

    when(() => mockHttpClient.get(any())).thenAnswer((_) async =>
        Fixture.createHttpResponse(file: 'stations.json', code: 200));

    when(() => mockGeoLocatorWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(true));
    when(() => mockGeoLocatorWrapper.checkPermission())
        .thenAnswer((_) async => Future.value(LocationPermission.always));

    when(() => mockGetLocationUseCase.execute())
        .thenAnswer((_) async => position);
    when(() => mockGetStationUseCase.execute()).thenAnswer((_) async =>
        Result.success(Fixture.createStations(file: 'stations.json')));
    when(() => mockGetAirQualityUseCase.execute(organisation: "revolvair"))
        .thenAnswer((_) async =>
            Result.success(Fixture.createRanges(file: 'ranges.json')));
    when(() => mockGetStationValue24HUsecase.execute()).thenAnswer((_) async =>
        Result.success(
            Fixture.createStationsValues(file: 'stationsValues.json')));
    when(() => mockTokenManager.getToken())
        .thenAnswer((_) => 'untokenpeuimportetantquecestpasvide');

    await tester.pumpWidget(
      EasyLocalization(
        supportedLocales: const [Locale('fr', 'CA')],
        path: 'assets/translations',
        startLocale: const Locale('fr', 'CA'),
        child: const MaterialApp(
          home: HomeView(),
        ),
      ),
    );

    await tester.pumpAndSettle();

    var drawer = find.byTooltip("Open navigation menu");
    await tester.tap(drawer);
    await tester.pumpAndSettle();

    final taskTextFinder =
        find.text(LocaleKeys.app_text_drawer_deconnexion.tr());
    expect(taskTextFinder, findsOneWidget);
  });
}
