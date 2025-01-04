import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class AuthError extends Failure {
  AuthError({String? message, super.context})
      : super(
            message: message ??
                "Une erreur est survenue avec l'authentification, veuillez vérifier votre connexion et réessayer.");
}

class LogoutError extends Failure {
  LogoutError({super.context})
      : super(message: LocaleKeys.failures_error_auth_logout.tr());
}

class ConnectionStatusError extends Failure {
  ConnectionStatusError({super.context})
      : super(message: LocaleKeys.failures_error_auth_connexion.tr());
}

class LoginError extends Failure {
  LoginError({super.context})
      : super(message: LocaleKeys.failures_error_auth_information.tr());
}
