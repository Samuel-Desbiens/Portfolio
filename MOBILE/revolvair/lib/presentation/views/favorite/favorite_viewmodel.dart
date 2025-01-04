import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/domain/entities/stations.dart';
import 'package:resolvair/domain/usecases/get_favorite_station_use_case.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app.router.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class FavoriteViewModel extends BaseViewModel {
  final GetFavoriteStationUseCase getFavoriteStation =
      locator<GetFavoriteStationUseCase>();
  final _dialogService = locator<DialogService>();
  final _navigationService = locator<NavigationService>();

  initialize() {
    fetchLiked();
  }

  List<Station> stations = [];

  Future<void> fetchLiked() async {
    setBusy(true);
    final result = await getFavoriteStation.execute();
    result.when(
        (value) => stations = value,
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  navigateToStationStatsPage({required String slug}) async {
    await _navigationService.navigateTo(Routes.stationDetailsView,
        arguments: slug);
        fetchLiked();
  }

  Color getColor(double? index) {
    if (index! >= 0 && index <= 12) {
      return Colors.green;
    } else if (index >= 12 && index <= 35) {
      return Colors.yellow;
    } else if (index >= 35 && index <= 55) {
      return Colors.orange;
    } else if (index >= 55 && index <= 150) {
      return Colors.red;
    } else if (index >= 150 && index <= 250) {
      return Colors.red.shade700;
    } else if (index >= 250 && index <= 10000) {
      return Colors.red.shade900;
    } else {
      return Colors.grey;
    }
  }
}
