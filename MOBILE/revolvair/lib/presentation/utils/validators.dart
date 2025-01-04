import 'package:easy_localization/easy_localization.dart';
import 'package:email_validator/email_validator.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class Validators {
  static String? validateName(String? value) {
    if (value == null || value.isEmpty) {
      return LocaleKeys.authentification_empty_name.tr();
    }
    return null;
  }

  static String? validatePassword(String? value) {
    final pattern = RegExp(
      r'^(?=.*[A-Z]).{8,}$',
    );
    bool passwordValid = pattern.hasMatch(value!);
    if (value.isEmpty) {
      return LocaleKeys.authentification_empty_password.tr();
    }
    if (!passwordValid) {
      return LocaleKeys.authentification_invalid_password.tr();
    }
    return null;
  }

  static String? validateEmail(String? value) {
    bool emailValid = EmailValidator.validate(value!);
    if (value.isEmpty) {
      return LocaleKeys.authentification_empty_email.tr();
    }
    if (!emailValid) {
      return LocaleKeys.authentification_invalid_email.tr();
    }
    return null;
  }
}
