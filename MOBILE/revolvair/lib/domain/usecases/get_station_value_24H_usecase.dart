import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/stationsValue.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class GetStationValue24HUsecase {
  final RevolvairServiceInterface apiRest;

  GetStationValue24HUsecase({required this.apiRest});

  Future<Result<List<StationValue>, Failure>> execute() async {
    var stationList = await apiRest.getStations24HValue();
    return stationList;
  }
}
