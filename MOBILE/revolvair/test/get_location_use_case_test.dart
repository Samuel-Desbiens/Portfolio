import 'dart:async';

import 'package:geolocator/geolocator.dart';
import 'package:latlong2/latlong.dart';
import 'package:mocktail/mocktail.dart';
import 'package:resolvair/data/locator_service_impl.dart';
import 'package:resolvair/data/wrapper/geolocator_wrapper.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/permission_failure.dart';
import 'package:resolvair/domain/usecases/get_location_use_case.dart';
import 'package:test/test.dart';

class MockGeoLocatorWrapper extends Mock implements GeoLocatorWrapper {}

Future<void> main() async {
  late GeoLocatorWrapper mockWrapper;
  late LocatorService apiService;
  late GetLocationUseCase locationUseCase;

  setUpAll(() async {
    mockWrapper = MockGeoLocatorWrapper();
    apiService = LocatorService(wrapper: mockWrapper);
    locationUseCase = GetLocationUseCase(apiLocation: apiService);
  });

  test('should return a position when execute is called successfully',
      () async {
    when(() => mockWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(true));
    when(() => mockWrapper.checkPermission())
        .thenAnswer((_) async => Future.value(LocationPermission.always));
    when(() => mockWrapper.getCurrentPosition())
        .thenAnswer((_) async => Future.value(Position(
              latitude: 46.78694341290782,
              longitude: -71.2848928696294,
              altitude: 0.0,
              speed: 0.0,
              accuracy: 0.0,
              heading: 0.0,
              speedAccuracy: 0.0,
              timestamp: DateTime.now(),
              altitudeAccuracy: 0.0,
              headingAccuracy: 0.0,
            )));

    var result = await locationUseCase.execute();

    final position = result.when(
      (position) => LatLng(position.latitude, position.longitude),
      (failure) => throw 'Expected position, but received failure: $failure',
    );

    expect(position, const LatLng(46.78694341290782, -71.2848928696294));
  });

  test('should return a LocationError when isLocationServiceEnabled is false',
      () async {
    when(() => mockWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(false));

    var result = await locationUseCase.execute();

    expect(result.tryGetError(), isA<LocationError>());
  });

  test(
      'should return a PermissionErrorDenied when PermissionErrorDenied is LocationPermission.denied',
      () async {
    when(() => mockWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(true));
    when(() => mockWrapper.checkPermission())
        .thenAnswer((_) async => Future.value(LocationPermission.denied));
    when(() => mockWrapper.requestPermission())
        .thenAnswer((_) async => Future.value(LocationPermission.denied));
    var result = await locationUseCase.execute();

    expect(result.tryGetError(), isA<PermissionErrorDenied>());
  });

  test(
      'should return a PermissionErrorDenied when PermissionErrorDeniedForeer is LocationPermission.deniedForever',
      () async {
    when(() => mockWrapper.isLocationServiceEnabled())
        .thenAnswer((_) async => Future.value(true));
    when(() => mockWrapper.checkPermission()).thenAnswer(
        (_) async => Future.value(LocationPermission.deniedForever));
    when(() => mockWrapper.requestPermission()).thenAnswer(
        (_) async => Future.value(LocationPermission.deniedForever));
    var result = await locationUseCase.execute();

    expect(result.tryGetError(), isA<PermissionErrorPermanent>());
  });
}
