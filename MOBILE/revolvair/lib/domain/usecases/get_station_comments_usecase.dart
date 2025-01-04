import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/comment.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/stationdetails_service.dart';

class GetStationCommentsUseCase {
  final StationDetailsServiceInterface detailApi;

  GetStationCommentsUseCase({required this.detailApi});

  Future<Result<List<Comments>, Failure>> execute(
      {required String slug}) async {
    var comments = await detailApi.getStationComments(slug: slug);
    return comments;
  }
}
