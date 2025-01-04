import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/usecases/get_statistiques_usecase.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class StatistiqueViewModel extends BaseViewModel {
  late GetStatistiqueUseCase getStatistiqueUseCase =
      locator<GetStatistiqueUseCase>();
  final dialogService = locator<DialogService>();

  late String slug;
  List<String> statsList = [
    "0.0",
    "0.0",
    "0.0",
    "0.0",
    "0.0",
    "0.0",
    "0.0",
    "0.0"
  ];
  
  final List<String> labels = [
    LocaleKeys.app_text_valeur_actuel.tr(),
    LocaleKeys.app_text_moyenne_24.tr(),
    LocaleKeys.app_text_moyenne_7.tr(),
    LocaleKeys.app_text_moyenne_mois.tr(),
    LocaleKeys.app_text_moyenne_max_24.tr(),
    LocaleKeys.app_text_moyenne_min_24.tr(),
    LocaleKeys.app_text_moyenne_max_semaine.tr(),
    LocaleKeys.app_text_moyenne_min_semaine.tr()
  ];

  Future<void> initialize(String param) async {
    slug = param;
    await fetchStationStats();
  }

  Future<void> fetchStationStats() async {
    setBusy(true);
    final result = await getStatistiqueUseCase.execute(slug: slug);
    result.when(
        (value) => statsList = [
              value.lastValue,
              value.averageDay,
              value.averageWeek,
              value.averageMonth,
              value.maxDay,
              value.minDay,
              value.maxWeek,
              value.minWeek
            ],
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }
}
