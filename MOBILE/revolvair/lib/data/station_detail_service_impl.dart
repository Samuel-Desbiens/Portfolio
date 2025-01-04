import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:http/http.dart' as http;
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/utils/constant.dart';
import 'package:resolvair/domain/entities/alert.dart';
import 'package:resolvair/domain/entities/comment.dart';
import 'package:resolvair/domain/entities/values.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/stationdetails_service.dart';
import 'dart:convert' as convert;

class StationDetailsService implements StationDetailsServiceInterface {
  final http.Client httpClient;

  StationDetailsService({required this.httpClient});

  final apiUrlAqi = dotenv.env['API_URL_AQI'];
  final stationUri = dotenv.env['API_STATION'];
  final commentStart = Constants.API_COMMENT;
  final commentAddon = Constants.COMMENT_END;

  @override
  Future<Result<List<String>, Failure>> getAverageValue(
      {required String slug}) async {
    try {
      const int weekLength = 7;
      var url = Uri.parse('$stationUri/$slug/measures/pm25_raw/average/24h');
      http.Response response = await httpClient.get(url);
      if (response.statusCode == 200) {
        final List<dynamic> jsonData =
            convert.json.decode(response.body)['data'];
        final List<Values> valueList =
            jsonData.map((data) => Values.fromJson(data)).toList();
        double averageWeek = 0;
        double averageMonth = 0;
        for (int i = 0; i < valueList.length; i++) {
          if (i < weekLength) {
            averageWeek += valueList[i].value;
          }
          averageMonth += valueList[i].value;
        }
        averageWeek /= weekLength;
        averageMonth /= valueList.length;
        return Success([
          valueList[0].value.toStringAsFixed(1),
          averageWeek.toStringAsFixed(1),
          averageMonth.toStringAsFixed(1)
        ]);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List<String>, Failure>> getMinMaxValue(
      {required String slug, required TimeStamp timeStamp}) async {
    try {
      String timeRange = "";
      switch (timeStamp) {
        case TimeStamp.day:
          timeRange = "24h";
        case TimeStamp.week:
          timeRange = "week";
        case TimeStamp.month:
          return Error(ResponseFailure());
      }
      var url = Uri.parse('$stationUri/$slug/measures/pm25/$timeRange/peaks');
      http.Response response = await httpClient.get(url);

      if (response.statusCode == 200) {
        final List<dynamic> jsonData =
            convert.json.decode(response.body)['data'];
        List<String> minmax = ["N/A", "N/A"];
        if (jsonData.isEmpty) {
          return Success(minmax);
        }
        final List<Values> valueList =
            jsonData.map((data) => Values.fromJson(data)).toList();
        if (valueList.length == 1) {
          minmax[0] = valueList[0].value.toStringAsFixed(1);
        } else if (valueList.length == 2) {
          minmax[0] = valueList[0].value.toStringAsFixed(1);
          minmax[1] = valueList[1].value.toStringAsFixed(1);
        }
        return Success(minmax);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<String, Failure>> getLastValue({required String slug}) async {
    try {
      var url = Uri.parse('$stationUri/$slug/measures/pm25/last');
      http.Response response = await httpClient.get(url);
      if (response.statusCode == 200) {
        final Map<String, dynamic> jsonmap =
            convert.jsonDecode(response.body) as Map<String, dynamic>;
        return Success(
            Values.fromJson(jsonmap['data']).value.toStringAsFixed(1));
      }
      return Error(ResponseFailure());
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List, Failure>> getAlerts(
      {required String slug, required int pageNumber}) async {
    var url = Uri.parse('$stationUri/$slug/alerts/details?page=$pageNumber');

    try {
      http.Response response = await httpClient.get(url);

      if (response.statusCode == 200) {
        List<dynamic> jsonData = convert.json.decode(response.body)['data'];
        Map jsonMeta = convert.json.decode(response.body)['meta'];

        final List<Alertes> listData =
            jsonData.map((jsonData) => Alertes.fromJson(jsonData)).toList();

        var listMeta = [];

        jsonMeta.forEach((key, value) {
          listMeta.add(value);
        });

        List<dynamic> listCombinedList = [listData, listMeta];

        return Success(listCombinedList);
      }
      List<String> emptyList = ["Aucune alertes"];
      return Success(emptyList);
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List<Comments>, Failure>> getStationComments(
      {required String slug}) async {
    var url = Uri.parse('$commentStart/$slug$commentAddon');

    try {
      http.Response response = await httpClient.get(url);
      if (response.statusCode == 200) {
        List<dynamic> jsonData = convert.json.decode(response.body)['data'];
        final List<Comments> listComments =
            jsonData.map((jsonData) => Comments.fromJson(jsonData)).toList();

        return Success(listComments);
      }
      return Error(ResponseFailure());
    } catch (error) {
      return Error(ResponseFailure());
    }
  }
}
