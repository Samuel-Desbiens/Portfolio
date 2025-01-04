import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/entities/alert.dart';
import 'package:resolvair/domain/usecases/get_alertes_usecase.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class AlerteViewModel extends BaseViewModel {
  late GetAlertesUseCase getAlertesUseCase = locator<GetAlertesUseCase>();
  final dialogService = locator<DialogService>();

  late String slug;
  List<Alertes> listeAlertes = [];
  int wantedPage = 1;
  bool isThereMoreData = true;
  int currentPage = 1;
  int lastPage = 1;

  Future<void> initialize(String param) async {
    slug = param;
    await fetchStationStats();
    await fetchStationsData(wantedPage);
  }

  Future<void> fetchStationStats() async {
    setBusy(true);
    setBusy(false);
  }

  Future<void> fetchStationsData(int wantedPage) async {
    setBusy(true);
    final result = await getAlertesUseCase.getAlertesData(
        slug: slug, receivedPageNumber: wantedPage);
    result.when(
        (value) =>
            {listeAlertes += value[0], lastPage = value[1][2], currentPage++},
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  String returnDays(String receivedDateInString) {
    final receivedDate = DateTime.parse(receivedDateInString);
    final dateToday = DateTime.now();

    final daysDifference = dateToday.difference(receivedDate).inDays;

    return daysDifference.toString();
  }
}
