import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class LocationError extends Failure {
  LocationError({super.context})
      : super(message: LocaleKeys.failures_error_localisation.tr());
}
