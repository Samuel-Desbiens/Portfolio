import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/home/home_viewmodel.dart';
import 'package:stacked/stacked.dart';

class ConnectDisconnectButtonWidget extends ViewModelWidget<HomeViewModel> {
  const ConnectDisconnectButtonWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, HomeViewModel viewModel) {
    if (!viewModel.isConnected) {
      return ListTile(
          title: Text(
            LocaleKeys.app_text_drawer_connexion.tr(),
            style: const TextStyle(
              color: Colors.white,
            ),
          ),
          onTap: () {
            viewModel.navigateToLoginPage();
          });
    } else {
      return ListTile(
          title: Text(
            LocaleKeys.app_text_drawer_deconnexion.tr(),
            style: const TextStyle(
              color: Colors.white,
            ),
          ),
          onTap: () {
            viewModel.logout();
          });
    }
  }
}
