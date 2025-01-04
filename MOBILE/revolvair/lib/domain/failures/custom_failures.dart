import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

abstract class Failure {
  final String message;
  final String context;

  Failure({this.context = '', this.message = ''});
}

class LocationError extends Failure {
  LocationError({super.context})
      : super(message: LocaleKeys.failures_error_localisation.tr());
}
