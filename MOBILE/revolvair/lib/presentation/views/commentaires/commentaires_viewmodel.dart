import 'package:easy_localization/easy_localization.dart';
import 'package:resolvair/domain/entities/comment.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_station_comments_usecase.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app.router.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class CommentairesViewModel extends BaseViewModel {
  final dialogService = locator<DialogService>();
  final _navigationService = locator<NavigationService>();
  final TokenManagerInterface tokenManager = locator<TokenManagerInterface>();
  GetStationCommentsUseCase getStationCommentsUseCase =
      locator<GetStationCommentsUseCase>();

  late String receivedSlug;
  late List<Comments> comments = [];

  Future<void> initialize(String param) async {
    receivedSlug = param;
    await fetchComments(receivedSlug);
  }

  Future<void> fetchComments(String slug) async {
    setBusy(true);
    final result = await getStationCommentsUseCase.execute(slug: slug);
    result.when(
        (value) => comments = value,
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
  }

  bool isAuthenticated = false;

  navigateToAddCommentPage({required String slug}) async {
    await _navigationService.navigateTo(Routes.ajoutCommentaireView,
        arguments: AjoutCommentaireViewArguments(slug: slug));
    fetchComments(receivedSlug);
  }

  void checkAuthentication() {
    if (tokenManager.getToken() != '') {
      navigateToAddCommentPage(slug: receivedSlug);
    } else {
      dialogService.showDialog(
          title: LocaleKeys.app_text_error.tr(),
          description: LocaleKeys.app_text_ajout_comment.tr());
    }
  }
}
