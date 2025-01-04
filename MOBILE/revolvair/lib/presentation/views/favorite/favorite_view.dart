import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/favorite/favorite_viewmodel.dart';
import 'package:stacked/stacked.dart';

class FavoriteView extends StatelessWidget {
  const FavoriteView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<FavoriteViewModel>.reactive(
      viewModelBuilder: () => FavoriteViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(),
      builder: (context, viewModel, child) {
        final isLoading = viewModel.isBusy;
        final favoriteStations = viewModel.stations;

        return Scaffold(
          appBar: AppBar(
            backgroundColor: Colors.black,
            title: Text(LocaleKeys.app_text_favorite_station_suivi.tr()),
          ),
          body: isLoading
              ? const Center(
                  child: CircularProgressIndicator(),
                )
              : favoriteStations.isNotEmpty
                  ? ListView.builder(
                      itemCount: favoriteStations.length,
                      itemBuilder: (context, index) {
                        final station = favoriteStations[index];
                        return ListTile(
                          leading: Container(
                            width: 45,
                            height: 45,
                            decoration: BoxDecoration(
                              shape: BoxShape.circle,
                              color: viewModel
                                  .getColor(station.latestMeasureValue),
                            ),
                            child: const Center(
                              child:
                                  Icon(Icons.location_on, color: Colors.white),
                            ),
                          ),
                          title: Text(station.name),
                          onTap: () {
                            viewModel.navigateToStationStatsPage(
                                slug: station.slug);
                          },
                        );
                      },
                    )
                  : Center(
                      child: Text(LocaleKeys.app_text_station_suivi.tr()),
                    ),
        );
      },
    );
  }
}
