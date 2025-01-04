import 'package:geolocator/geolocator.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';

abstract class LocationServiceInterface {
  Future<Result<Position, Failure>> getUserPosition();
}
