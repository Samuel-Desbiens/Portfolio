import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/domain/usecases/get_air_quality_use_case.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class IqaViewModel extends BaseViewModel {
  late GetAirQualityUseCase getAirQualityUseCase =
      locator<GetAirQualityUseCase>();
  final dialogService = locator<DialogService>();

  Future<void> initialize() async {
    await fetchRanges();
  }

  List<Ranges> ranges = [];

  Future<void> fetchRanges() async {
    setBusy(true);
    final result = await getAirQualityUseCase.execute(organisation: "usepa");
    result.when(
        (value) => ranges = value,
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }
}
