import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/entities/stationStats.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/stationdetails_service.dart';

class GetStatistiqueUseCase {
  final StationDetailsServiceInterface apiRest;

  GetStatistiqueUseCase({required this.apiRest});

  Future<Result<StationStats, Failure>> execute({required String slug}) async {
    try {
      bool lastValError = false;
      bool averageError = false;
      bool minmaxDayError = false;
      bool minmaxMonthError = false;
      String lastValue = "N/A";
      List<String> listAverage = ["N/A", "N/A", "N/A"];
      List<String> minmax24 = ["N/A", "N/A"]; 
      List<String> minmax7 = ["N/A", "N/A"];

      Result<String, Failure> lastVal = await apiRest.getLastValue(slug: slug);
      lastVal.when(
        (value) => lastValue = value,
        (error) async => lastValError = true);

      Result<List<String>, Failure> average = await apiRest.getAverageValue(slug: slug);
      average.when(
        (value) => listAverage = value,
        (error) async => averageError = true);

      Result<List<String>, Failure> minmaxDay = await apiRest.getMinMaxValue(slug: slug, timeStamp: TimeStamp.day);
      minmaxDay.when(
        (value) => minmax24 = value,
        (error) async => minmaxDayError = true);

      Result<List<String>, Failure> minmaxWeek = await apiRest.getMinMaxValue(slug: slug, timeStamp: TimeStamp.week);
      minmaxWeek.when(
        (value) => minmax7 = value,
        (error) async =>  minmaxMonthError = true);

      if(lastValError | averageError | minmaxDayError | minmaxMonthError){
        return Error(ResponseFailure());
      }
      StationStats stats = StationStats(
        lastValue: lastValue,
        averageDay: listAverage[0],
        averageWeek: listAverage[1],
        averageMonth: listAverage[2],
        maxDay: minmax24[0],
        minDay: minmax24[1],
        maxWeek: minmax7[0],
        minWeek: minmax7[1]);
      return Success(stats);
    } catch (error) {
      return Error(ResponseFailure());
    }
  }
}
