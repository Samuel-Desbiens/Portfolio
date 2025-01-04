import 'dart:convert';
import 'dart:io';
import 'package:geolocator/geolocator.dart';
import 'package:http/http.dart' as http;
import 'package:resolvair/domain/entities/comment.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/entities/stationStats.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/entities/alert.dart';
import 'package:resolvair/domain/entities/stationsValue.dart';

class Fixture {
  static List<Ranges> createRanges({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final List<dynamic> rangeJsonList = json.decode(jsonString)['ranges'];
    // Convert the list of dynamic JSON objects to a list of Ranges objects
    final List<Ranges> ranges =
        rangeJsonList.map((json) => Ranges.fromJson(json)).toList();
    return ranges;
  }

  static List<Station> createStations({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final List<dynamic> stationJsonList = json.decode(jsonString)['data'];
    // Convert the list of dynamic JSON objects to a list of Ranges objects
    final List<Station> station =
        stationJsonList.map((json) => Station.fromJson(json)).toList();
    return station;
  }

  static List<StationValue> createStationsValues({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final List<dynamic> stationValuesJsonList = json.decode(jsonString)['data'];
    // Convert the list of dynamic JSON objects to a list of Ranges objects
    final List<StationValue> stationvalues = stationValuesJsonList
        .map((json) => StationValue.fromJson(json))
        .toList();
    return stationvalues;
  }

  static List<Comments> createComments({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final List<dynamic> stationCommentsJsonList =
        json.decode(jsonString)['data'];
    final List<Comments> commentvalues =
        stationCommentsJsonList.map((json) => Comments.fromJson(json)).toList();
    return commentvalues;
  }

  static Future<http.Response> createHttpResponse(
      {required String file, required int code}) async {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    return http.Response(jsonString, code);
  }

  static StationStats createStationStats({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final Map<String, dynamic> stationStatsJson = json.decode(jsonString);
    return StationStats.fromJson(stationStatsJson['data']);
  }

  static List<Station> createFavorite({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final List<dynamic> rangeJsonList = json.decode(jsonString)['data'];
    final List<Station> ranges =
        rangeJsonList.map((json) => Station.fromJson(json)).toList();
    return ranges;
  }

  static Alertes createAlertStats({required String file}) {
    final jsonString = File('test/fixtures/$file').readAsStringSync();
    final Map<String, dynamic> alertJson = json.decode(jsonString);
    return Alertes.fromJson(alertJson);
  }

  static Position createExamplePositions() {
    return Position(
      latitude: 46.78694341290782,
      longitude: -71.2848928696294,
      altitude: 0.0,
      speed: 0.0,
      accuracy: 0.0,
      heading: 0.0,
      speedAccuracy: 0.0,
      timestamp: DateTime.now(),
      altitudeAccuracy: 0.0,
      headingAccuracy: 0.0,
    );
  }
}
