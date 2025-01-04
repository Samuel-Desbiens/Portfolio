import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class GetAirQualityUseCase {
  final RevolvairServiceInterface apiRest;

  GetAirQualityUseCase({required this.apiRest});

  Future<Result<List<Ranges>, Failure>> execute(
      {required String organisation}) async {
    var rangesList =
        await apiRest.getAirQualityRanges(organisation: organisation);
    return rangesList;
  }
}
