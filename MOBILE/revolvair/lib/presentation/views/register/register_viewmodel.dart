import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/widgets.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/post_register_use_case.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app_locator.dart';

import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class RegisterViewModel extends BaseViewModel {
  final _navigationService = locator<NavigationService>();
  final TextEditingController controllerEmail = TextEditingController();
  final TextEditingController controllerNom = TextEditingController();
  final TextEditingController controllerPass = TextEditingController();
  late TokenManagerInterface tokenManager = locator<TokenManagerInterface>();

  late PostRegisterUseCase authUseCase = locator<PostRegisterUseCase>();

  void initialize() {}

  Future<void> register() async {
    setBusy(true);
    final dialogService = locator<DialogService>();
    var result = await authUseCase.execute(
        controllerNom.text, controllerEmail.text, controllerPass.text);
    result.when(
        (value) => tokenManager.setToken(value),
        (error) async => await dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
    setBusy(false);
    if (tokenManager.getToken() != '') {
      _navigationService.popRepeated(2);
    }
  }
}
