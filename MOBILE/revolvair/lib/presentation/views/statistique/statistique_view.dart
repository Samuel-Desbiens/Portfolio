import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/statistique/statistique_viewmodel.dart';
import 'package:resolvair/presentation/widgets/grid_widget.dart';
import 'package:stacked/stacked.dart';

class StatistiqueView extends StatelessWidget {
  const StatistiqueView({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments;
    return ViewModelBuilder<StatistiqueViewModel>.reactive(
      viewModelBuilder: () => StatistiqueViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(args.toString()),
      builder: (context, viewModel, child) {
        return Scaffold(
        body:Column(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Container(
                alignment: Alignment.topCenter,
                child: Text(
                  LocaleKeys.app_text_statistique_air_quality.tr(),
                  style: const TextStyle(
                    fontSize: 18.0,
                    fontWeight: FontWeight.w100,
                  ),
                ),
              ),
              Expanded(
                child: Container(
                  child: GridWidget(viewModel: viewModel),
                ),
              ),
              Container(
                alignment: Alignment.bottomCenter,
                child: Text(
                  LocaleKeys.app_text_statistique_air_particules.tr(),
                  style: const TextStyle(
                    fontSize: 18.0,
                    fontWeight: FontWeight.w100,
                  ),
                ),
              ),
            ],
          ),
        );
      },
    );
  }
}
