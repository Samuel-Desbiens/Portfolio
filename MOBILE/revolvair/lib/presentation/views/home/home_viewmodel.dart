import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:latlong2/latlong.dart';
import 'package:resolvair/data/wrapper/geolocator_wrapper.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/entities/stationsValue.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_air_quality_use_case.dart';
import 'package:resolvair/domain/usecases/logout_usecase.dart';
import 'package:resolvair/domain/usecases/get_location_use_case.dart';
import 'package:resolvair/domain/usecases/get_station_use_case.dart';
import 'package:resolvair/domain/usecases/load_token_use_case.dart';
import 'package:resolvair/domain/usecases/store_token_use_case.dart';
import 'package:resolvair/domain/usecases/get_station_value_24H_usecase.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app.router.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class HomeViewModel extends BaseViewModel {
  final _navigationService = locator<NavigationService>();
  final GetStationUseCase getStationUseCase = locator<GetStationUseCase>();
  final _dialogService = locator<DialogService>();
  final getLocationUseCase = locator<GetLocationUseCase>();
  final _location = locator<GeoLocatorWrapper>();
  final MapController mapController = MapController();
  final LoadTokenUseCase loadTokenUseCase = locator<LoadTokenUseCase>();
  final StoreTokenUseCase storeTokenUseCase = locator<StoreTokenUseCase>();
  late GetAirQualityUseCase getAirQualityUseCase =
      locator<GetAirQualityUseCase>();
  final GetStationValue24HUsecase getStationValue24HUsecase =
      locator<GetStationValue24HUsecase>();
  final TokenManagerInterface _tokenManager = locator<TokenManagerInterface>();
  final getLogoutUseCase = locator<GetAuthLogoutUseCase>();
  bool hasSearched = false;
  List<double> coordinates = [];

  bool isConnected = false;

  List<Ranges> revolvairRanges = [];
  List<Station> stations = [];
  List<StationValue> stationsValues = [];

  Future<void> initialize() async {
    await fetchPosition();
    await fetchStation();
    await fetchRevolvairRanges();
    await fetchStationValue24H();
    isConnected = checkConnection();
  }

  navigateToAirPage() async {
    await _navigationService.navigateTo(Routes.airView,
        arguments: AirViewArguments(revolvairRanges: revolvairRanges));
  }

  navigateToLoginPage() async {
    await _navigationService.navigateTo(Routes.loginView);
    isConnected = checkConnection();
    notifyListeners();
  }

  navigateToSearchPage() async {
    coordinates = await _navigationService.navigateTo(Routes.searchView);
    mapController.move(LatLng(coordinates[0], coordinates[1]), 10);
  }

  navigateToStationStatsPage({required String slug}) async {
    await _navigationService.navigateTo(Routes.stationDetailsView,
        arguments: slug);
  }

  Future<void> fetchStation() async {
    setBusy(true);
    final result = await getStationUseCase.execute();
    result.when(
        (value) => stations = value,
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  LatLng position = const LatLng(46.78694341290782,
      -71.2848928696294); //pour centrer par defaut sur le cegep de ste-foy

  bool isLocalise = false;
  bool isMapMoved = false;

  Future<void> fetchPosition() async {
    final result = await getLocationUseCase.execute();
    result.when(
      (value) {
        position = LatLng(value.latitude, value.longitude);
        mapController.move(position, 14);
        isMapMoved = false;
        isLocalise = true;
      },
      (error) async {
        await _dialogService.showDialog(
          title: LocaleKeys.app_text_error.tr(),
          description: error.message.toString(),
        );
        position = const LatLng(46.78694341290782, -71.2848928696294);
        mapController.move(position, 14);
        isMapMoved = false;
        isLocalise = false;
      },
    );
    notifyListeners();
  }

  void handleMapMove() {
    isMapMoved = true;
    notifyListeners();
  }

  Future<void> handleGeolocalisationPermissions() async {
    var locationStatus = await _location.isLocationServiceEnabled();
    if (!locationStatus) {
      // Location services are off
      _location.requestPermission();
      _location.openLocationSettings();
    } else {
      // Location services are on
      isLocalise = true;
      isMapMoved = false;
    }
    fetchPosition();
    notifyListeners();
  }

  bool checkConnection() {
    if (_tokenManager.getToken() == '') {
      return false;
    }
    return true;
  }

  Future<void> fetchStationValue24H() async {
    setBusy(true);
    final result = await getStationValue24HUsecase.execute();
    result.when(
        (value) => stationsValues = value,
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  Future<void> fetchRevolvairRanges() async {
    setBusy(true);
    final result =
        await getAirQualityUseCase.execute(organisation: "revolvair");
    result.when(
        (value) => revolvairRanges = value,
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  Color get24HStationValuesColor(int id) {
    for (int i = 0; i < stationsValues.length; i++) {
      if (id == stationsValues[i].id) {
        for (int o = 0; o < revolvairRanges.length; o++) {
          if (stationsValues[i].value <= revolvairRanges[o].max) {
            return revolvairRanges[o].getColorFromHex();
          }
        }
      }
    }
    return Colors.grey;
  }

  navigateToFavoritePage() async {
    await _navigationService.navigateTo(Routes.favoriteView);
  }

  Future<void> storeToken() async {
    final result = await storeTokenUseCase.execute();
    result.when(
      (value) {},
      (error) async {
        await _dialogService.showDialog(
          title: LocaleKeys.app_text_error.tr(),
          description: error.message.toString(),
        );
      },
    );
    notifyListeners();
  }

  Future<void> loadToken() async {
    final result = await loadTokenUseCase.execute();
    result.when(
      (value) {},
      (error) async {
        await _dialogService.showDialog(
          title: LocaleKeys.app_text_error.tr(),
          description: error.message.toString(),
        );
      },
    );
    notifyListeners();
  }

  Future<void> logout() async {
    final result = await getLogoutUseCase.execute();
    result.when(
      (value) {
        isConnected = false;
      },
      (error) async {
        await _dialogService.showDialog(
          title: LocaleKeys.app_text_error.tr(),
          description: error.message.toString(),
        );
      },
    );
    notifyListeners();
  }
}
