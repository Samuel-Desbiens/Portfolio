import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/stationdetails_service.dart';

class GetAlertesUseCase {
  final StationDetailsServiceInterface apiRest;

  GetAlertesUseCase({required this.apiRest});

  Future<Result<List, Failure>> getAlertesData(
      {required String slug, required int receivedPageNumber}) async {
    var alertsList =
        await apiRest.getAlerts(slug: slug, pageNumber: receivedPageNumber);
    return alertsList;
  }
}
