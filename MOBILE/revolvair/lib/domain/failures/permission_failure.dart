import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class PermissionError extends Failure {
  PermissionError({super.context})
      : super(message: LocaleKeys.failures_error_permission.tr());
}

class PermissionErrorDenied extends Failure {
  PermissionErrorDenied({super.context})
      : super(
            message:
                LocaleKeys.failures_error_permission_localisation_refuse.tr());
}

class PermissionErrorPermanent extends Failure {
  PermissionErrorPermanent({super.context})
      : super(
            message: LocaleKeys.failures_error_permission_localisation_permanent
                .tr());
}
