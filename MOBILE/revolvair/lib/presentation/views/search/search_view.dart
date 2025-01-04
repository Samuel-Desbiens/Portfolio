import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:stacked/stacked.dart';
import 'package:resolvair/presentation/views/search/search_viewmodel.dart';
import 'package:easy_debounce/easy_debounce.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class SearchView extends StatelessWidget {
  const SearchView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<SearchViewModel>.reactive(
        viewModelBuilder: () => SearchViewModel(),
        onViewModelReady: (viewModel) => viewModel.initialize(),
        builder: (context, viewModel, child) {
          return Scaffold(
            appBar: AppBar(
              backgroundColor: Colors.black,
              title: Text(LocaleKeys.app_revolvair_text.tr()),
              bottom: Tab(
                  child: SearchBar(
                onChanged: (value) => EasyDebounce.debounce(
                    'osm-debouncer',
                    const Duration(milliseconds: 500),
                    () => viewModel.callOsm(value)),
              )),
            ),
            body: Stack(
              children: [
                Container(
                    padding: const EdgeInsets.all(20),
                    alignment: Alignment.bottomCenter,
                    child: ListView.builder(
                      itemCount: viewModel.locations.length,
                      itemBuilder: (context, index) {
                        final currentLocation = viewModel.locations[index];
                        return ListTile(
                          onTap: () {
                            viewModel.coordinates.add(currentLocation.lat);
                            viewModel.coordinates.add(currentLocation.lon);
                            Navigator.pop(context, viewModel.coordinates);
                          },
                          title: Text(currentLocation.displayName),
                        );
                      },
                    )),
              ],
            ),
          );
        });
  }
}
