import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class GetFavoriteStationUseCase {
  final RevolvairServiceInterface stationApi;

  GetFavoriteStationUseCase({required this.stationApi});

  Future<Result<List<Station>, Failure>> execute() async {
    final stationList = stationApi.getFavoriteStations();
    return stationList;
  }
}
