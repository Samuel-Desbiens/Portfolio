import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/entities/stationsValue.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';

abstract class RevolvairServiceInterface {
  Future<Result<List<Ranges>, Failure>> getAirQualityRanges(
      {required String organisation});
  Future<Result<List<Station>, Failure>> getStations();
  Future<Result<String, Failure>> postComment(
      String commentContent, String slug);
  Future<Result<List<StationValue>, Failure>> getStations24HValue();
  Future<Result<List<Station>, Failure>> getFavoriteStations();
  Future<Result<Unit, Failure>> addFavoriteStation({required String slug});
}
