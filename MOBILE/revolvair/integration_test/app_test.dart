import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:resolvair/main.dart';

Future<void> main() async {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();
  AppSetup.setupLocator();

  testWidgets("Je veux me connecter/déconnecter", (tester) async {
    await tester.pumpWidget(
      EasyLocalization(
        supportedLocales: const [Locale('fr', 'CA')],
        path: 'assets/translations',
        startLocale: const Locale('fr', 'CA'),
        child: const MaterialApp(
          home: MainApp(),
        ),
      ),
    );
    await tester.pumpAndSettle();

    final Finder drawerIconFinder = find.byIcon(Icons.menu);
    await tester.tap(drawerIconFinder);
    await tester.pumpAndSettle();

    final Finder drawerItemFinder =
        find.text(LocaleKeys.app_text_drawer_connexion.tr());
    await tester.tap(drawerItemFinder);
    await tester.pumpAndSettle();

    final emailTextField =
        find.widgetWithText(TextFormField, LocaleKeys.app_text_courriel.tr());
    final passwordTextField =
        find.widgetWithText(TextFormField, LocaleKeys.app_text_password.tr());

    await tester.enterText(emailTextField, "testtest@hotmail.com");
    await tester.enterText(passwordTextField, "testtest1");

    await tester.pumpAndSettle();

    final loginButton =
        find.widgetWithText(ElevatedButton, LocaleKeys.app_text_login.tr());
    await tester.tap(loginButton);
    await tester.pumpAndSettle();

    expect(
        find.text(LocaleKeys.app_text_drawer_deconnexion.tr()), findsOneWidget);

    final Finder drawerDeco =
        find.text(LocaleKeys.app_text_drawer_deconnexion.tr());
    await tester.tap(drawerDeco);
    await tester.pumpAndSettle();

    expect(
        find.text(LocaleKeys.app_text_drawer_connexion.tr()), findsOneWidget);
  });

  //Pour faire fonctionner le test suivant, il faut :
  // 1 Lancé l'exécution
  // 2 Accepter l'autorisation de localisation.
  // 3 Kill l'exécution avec CTRL+C
  // 4 Relancer l'exécution et le test va passé
  testWidgets(
      "Drag l'application vers le bas puis clique sur le btn pour revenir sur notre position",
      (tester) async {
    await tester.pumpWidget(
      EasyLocalization(
        supportedLocales: const [Locale('fr', 'CA')],
        path: 'assets/translations',
        startLocale: const Locale('fr', 'CA'),
        child: const MaterialApp(
          home: MainApp(),
        ),
      ),
    );

    await tester.pumpAndSettle();

    final mapWidget = tester.widget<FlutterMap>(find.byType(FlutterMap));
    final finder = find.byWidgetPredicate((widget) => widget is FlutterMap);
    expect(finder, findsOneWidget);
    final startPosition = mapWidget.options.center;
    await tester.pumpAndSettle();
    await Future.delayed(const Duration(seconds: 2));
    await tester.drag(finder, const Offset(0, -500));
    await tester.pumpAndSettle();
    await Future.delayed(const Duration(seconds: 2));
    final middlePosition = mapWidget.options.onPointerCancel;
    expect(middlePosition, isNot(equals(startPosition)));
    final btn = find.byType(FloatingActionButton);
    await tester.tap(btn);
    await tester.pumpAndSettle();
    final lastPosition = mapWidget.options.center;
    expect(lastPosition, equals(startPosition));
  });
}
