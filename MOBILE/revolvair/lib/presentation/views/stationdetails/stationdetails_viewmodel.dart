import 'package:easy_localization/easy_localization.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_alertes_usecase.dart';
import 'package:resolvair/domain/usecases/post_favorite_use_case.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class StationDetailsViewModel extends BaseViewModel {
  late GetAlertesUseCase getAlertesUseCase = locator<GetAlertesUseCase>();
  late PostFavoriteUseCase addFavoriteUseCase = locator<PostFavoriteUseCase>();
  late TokenManagerInterface tokenManager = locator<TokenManagerInterface>();
  final dialogService = locator<DialogService>();

  late String slug;
  String alertsQte = "0";
  late String token = '';

  Future<void> initialize(String param) async {
    slug = param;
    token = tokenManager.getToken();
    await fetchStationStats();
  }

  Future<void> fetchStationStats() async {
    setBusy(true);
    final result = await getAlertesUseCase.getAlertesData(
        slug: slug, receivedPageNumber: 1);
    result.when(
        (value) => alertsQte = value[1][6].toString(),
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  Future<void> addToFavorite() async {
    setBusy(true);
    final result = await addFavoriteUseCase.execute(slug: slug);
    result.when(
        (value) => unit,
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }
}
