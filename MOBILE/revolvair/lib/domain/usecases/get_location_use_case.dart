import 'package:geolocator/geolocator.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/location_service.dart';

class GetLocationUseCase {
  final LocationServiceInterface apiLocation;

  GetLocationUseCase({required this.apiLocation});

  Future<Result<Position, Failure>> execute() async {
    var position = apiLocation.getUserPosition();
    return position;
  }
}
