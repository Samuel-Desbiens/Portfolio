import 'package:easy_localization/easy_localization.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/utils/validators.dart';

void main() {
  group('Validators', () {
    test('validateName - nom valide', () {
      final result = Validators.validateName('John');
      expect(result, isNull);
    });

    test('validateName - nom vide', () {
      final result = Validators.validateName('');
      expect(result, LocaleKeys.authentification_empty_name.tr());
    });

    test('validatePassword - mot de passe valide', () {
      final result = Validators.validatePassword('Password1');
      expect(result, isNull);
    });

    test('validatePassword - mot de passe vide', () {
      final result = Validators.validatePassword('');
      expect(result, LocaleKeys.authentification_empty_password.tr());
    });

    test('validatePassword - mot de passe invalid', () {
      final result = Validators.validatePassword('invalid');
      expect(result, LocaleKeys.authentification_invalid_password.tr());
    });

    test('validateEmail - email valide', () {
      final result = Validators.validateEmail('example@example.com');
      expect(result, isNull);
    });

    test('validateEmail - email vide', () {
      final result = Validators.validateEmail('');
      expect(result, LocaleKeys.authentification_empty_email.tr());
    });

    test('validateEmail - email invalide', () {
      final result = Validators.validateEmail('invalid-email');
      expect(result, LocaleKeys.authentification_invalid_email.tr());
    });
  });
}
