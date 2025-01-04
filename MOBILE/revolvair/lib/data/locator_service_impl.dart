import 'package:geolocator/geolocator.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/wrapper/geolocator_wrapper.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/permission_failure.dart';
import 'package:resolvair/domain/services/location_service.dart';

class LocatorService implements LocationServiceInterface {
  final GeoLocatorWrapper wrapper;

  LocatorService({required this.wrapper});

  @override
  Future<Result<Position, Failure>> getUserPosition() async {
    bool serviceEnabled;
    LocationPermission permission;
    serviceEnabled = await wrapper.isLocationServiceEnabled();
    if (!serviceEnabled) {
      return Error(LocationError());
    }
    permission = await wrapper.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await wrapper.requestPermission();
      if (permission == LocationPermission.denied) {
        return Error(PermissionErrorDenied());
      }
    }
    if (permission == LocationPermission.deniedForever) {
      return Error(PermissionErrorPermanent());
    }
    try {
      return Success(await wrapper.getCurrentPosition());
    } catch (error) {
      return Error(LocationError());
    }
  }
}
