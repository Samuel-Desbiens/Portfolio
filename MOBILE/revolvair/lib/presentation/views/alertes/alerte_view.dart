import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/presentation/views/alertes/alerte_viewmodel.dart';
import 'package:stacked/stacked.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class AlerteView extends StatelessWidget {
  const AlerteView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments;
    return ViewModelBuilder<AlerteViewModel>.reactive(
      viewModelBuilder: () => AlerteViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(args.toString()),
      builder: (context, viewModel, child) {
        final alertes = viewModel.listeAlertes;

        return Stack(children: [
          Container(
            padding: const EdgeInsets.all(10),
            child: ListView.builder(
              itemCount: alertes.length,
              itemBuilder: (context, index) {
                final alerte = alertes[index];
                return ListTile(
                  title: Text(LocaleKeys.app_text_alertes_date.tr(
                      namedArgs: {'date': viewModel.returnDays(alerte.date)})),
                  subtitle: Text(alerte.description),
                  textColor: Colors.black,
                );
              },
            ),
          ),
          if (viewModel.currentPage <= viewModel.lastPage)
            Positioned(
              bottom: 16.0,
              right: 16.0,
              child: FloatingActionButton(
                onPressed: () async {
                  viewModel.fetchStationsData(viewModel.wantedPage += 1);
                },
                child: const Icon(Icons.arrow_downward),
              ),
            )
        ]);
      },
    );
  }
}
