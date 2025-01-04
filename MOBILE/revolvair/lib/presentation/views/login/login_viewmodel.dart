import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/domain/usecases/logout_usecase.dart';
import 'package:resolvair/domain/usecases/login_usecase.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/app/app.router.dart';
import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';

class LoginViewModel extends BaseViewModel {
  Future<void> initialize() async {}
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  final _dialogService = locator<DialogService>();
  final GetAuthUseCase _getAuthUseCase = locator<GetAuthUseCase>();
  final _navigationService = locator<NavigationService>();
  final GetAuthLogoutUseCase _logoutUseCase = locator<GetAuthLogoutUseCase>();

  Future<void> login() async {
    final email = emailController.text;
    final password = passwordController.text;
    final result = await _getAuthUseCase.execute(email, password);
    result.when(
        (value) => {
              _navigationService.back(),
            },
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
  }

  navigateToRegisterPage() async {
    await _navigationService.navigateTo(Routes.registerView);
  }

  Future<void> logout() async {
    final result = await _logoutUseCase.execute();
    result.when(
        (success) => _dialogService.showDialog(
            title: LocaleKeys.app_deconnexion_title.tr(),
            description: LocaleKeys.app_text_deconnexion.tr()),
        (error) async => await _dialogService.showDialog(
              title: LocaleKeys.app_text_error.tr(),
              description: error.message.toString(),
            ));
  }
}
