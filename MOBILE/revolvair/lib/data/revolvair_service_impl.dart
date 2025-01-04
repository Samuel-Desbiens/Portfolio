import 'package:http/http.dart' as http;
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/entities/stationsValue.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'dart:convert';

import 'utils/constant.dart';

class RevolvairService implements RevolvairServiceInterface {
  final http.Client httpClient;
  final TokenManagerInterface tokenManager;

  RevolvairService({required this.httpClient, required this.tokenManager});

  final apiUrlAqi = Constants.API_URL_AQI;
  final stationUri = Constants.API_STATION;
  final station24HValue = Constants.API_LAST;
  final favoriteStation = Constants.URL_FAVORITE;
  final commentsUri = Constants.API_COMMENT;

  @override
  Future<Result<List<Ranges>, Failure>> getAirQualityRanges(
      {required String organisation}) async {
    try {
      final response =
          await httpClient.get(Uri.parse('$apiUrlAqi/$organisation'));
      if (response.statusCode == 200) {
        final List<dynamic> jsonData = json.decode(response.body)['ranges'];
        final List<Ranges> rangesList =
            jsonData.map((rangeData) => Ranges.fromJson(rangeData)).toList();
        return Success(rangesList);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List<Station>, Failure>> getStations() async {
    try {
      final response = await httpClient.get(Uri.parse(stationUri));
      if (response.statusCode == 200) {
        final List<dynamic> jsonData = json.decode(response.body)['data'];
        final List<Station> stationList = jsonData
            .map((stationData) => Station.fromJson(stationData))
            .toList();
        return Success(stationList);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List<StationValue>, Failure>> getStations24HValue() async {
    try {
      final response = await httpClient.get(Uri.parse(station24HValue));
      if (response.statusCode == 200) {
        final List<dynamic> jsonData = json.decode(response.body)['data'];
        final List<StationValue> stationValueList = jsonData
            .map((stationData) => StationValue.fromJson(stationData))
            .toList();
        return Success(stationValueList);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<List<Station>, Failure>> getFavoriteStations() async {
    try {
      final token = tokenManager.getToken();
      final response = await httpClient.get(
        Uri.parse(favoriteStation),
        headers: {
          'Authorization': 'Bearer $token',
        },
      );

      if (response.statusCode == 200) {
        final List<dynamic> data = json.decode(response.body)['data'];
        final List<Station> stations =
            data.map((item) => Station.fromJson(item)).toList();

        return Result.success(stations);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<Unit, Failure>> addFavoriteStation(
      {required String slug}) async {
    try {
      final token = tokenManager.getToken();
      final response = await httpClient.post(
        Uri.parse("$stationUri/$slug/affections"),
        headers: {
          'Authorization': 'Bearer $token',
        },
      );
      if (response.statusCode == 200 || response.statusCode == 201) {
        return const Success(unit);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
  }

  @override
  Future<Result<String, Failure>> postComment(
      String commentContent, String slug) async {
    final token = tokenManager.getToken();

    try {
      final headers = <String, String>{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer $token',
      };

      final response = await httpClient.post(
        Uri.parse('$commentsUri/$slug/comments'),
        headers: headers,
        body: json.encode({
          'text': commentContent,
          'status': "final",
        }),
      );

      if (response.statusCode == 201) {
        return const Result.success('Commentaire ajouté');
      } else {
        return Error(ResponseFailure());
      }
    } catch (e) {}
    throw Error(ResponseFailure());
  }
}
