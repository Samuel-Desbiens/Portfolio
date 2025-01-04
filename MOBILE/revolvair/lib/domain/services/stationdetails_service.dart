import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/comment.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';

enum TimeStamp { day, week, month }

abstract class StationDetailsServiceInterface {
  Future<Result<List<String>, Failure>> getAverageValue({required String slug});

  Future<Result<List<String>, Failure>> getMinMaxValue(
      {required String slug, required TimeStamp timeStamp});

  Future<Result<String, Failure>> getLastValue({required String slug});

  Future<Result<List<Comments>, Failure>> getStationComments(
      {required String slug});

  Future<Result<List, Failure>> getAlerts(
      {required String slug, required int pageNumber});
}
