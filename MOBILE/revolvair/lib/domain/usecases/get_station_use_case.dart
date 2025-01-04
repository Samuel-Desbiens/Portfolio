import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class GetStationUseCase {
  final RevolvairServiceInterface apiRest;

  GetStationUseCase({required this.apiRest});

  Future<Result<List<Station>, Failure>> execute() async {
    var stationList = await apiRest.getStations();
    return stationList;
  }
}
