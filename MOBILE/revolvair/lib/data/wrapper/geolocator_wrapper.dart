import 'package:geolocator/geolocator.dart';

class GeoLocatorWrapper {
  Future<LocationPermission> checkPermission() async {
    return Geolocator.checkPermission();
  }

  Future<LocationPermission> requestPermission() async {
    return Geolocator.requestPermission();
  }

  Future<bool> isLocationServiceEnabled() async {
    return Geolocator.isLocationServiceEnabled();
  }

  Future<Position?> getLastKnownPosition({
    bool forceAndroidLocationManager = false,
  }) async {
    return Geolocator.getLastKnownPosition(
      forceAndroidLocationManager: forceAndroidLocationManager,
    );
  }

  Future<Position> getCurrentPosition({
    LocationAccuracy desiredAccuracy = LocationAccuracy.best,
    bool forceAndroidLocationManager = false,
    Duration? timeLimit,
  }) async {
    return Geolocator.getCurrentPosition(
      desiredAccuracy: desiredAccuracy,
      forceAndroidLocationManager: forceAndroidLocationManager,
      timeLimit: timeLimit,
    );
  }

  Stream<Position> getPositionStream({
    LocationSettings? locationSettings,
  }) {
    return Geolocator.getPositionStream(locationSettings: locationSettings);
  }

  Future<LocationAccuracyStatus> getLocationAccuracy() async {
    return Geolocator.getLocationAccuracy();
  }

  Stream<ServiceStatus> getServiceStatusStream() {
    return Geolocator.getServiceStatusStream();
  }

  Future<LocationAccuracyStatus> requestTemporaryFullAccuracy({
    required String purposeKey,
  }) async {
    return Geolocator.requestTemporaryFullAccuracy(
      purposeKey: purposeKey,
    );
  }

  Future<bool> openAppSettings() async {
    return Geolocator.openAppSettings();
  }

  Future<bool> openLocationSettings() async {
    return Geolocator.openLocationSettings();
  }

  double distanceBetween(
    double startLatitude,
    double startLongitude,
    double endLatitude,
    double endLongitude,
  ) {
    return Geolocator.distanceBetween(
      startLatitude,
      startLongitude,
      endLatitude,
      endLongitude,
    );
  }

  double bearingBetween(
    double startLatitude,
    double startLongitude,
    double endLatitude,
    double endLongitude,
  ) {
    return Geolocator.bearingBetween(
      startLatitude,
      startLongitude,
      endLatitude,
      endLongitude,
    );
  }
}
