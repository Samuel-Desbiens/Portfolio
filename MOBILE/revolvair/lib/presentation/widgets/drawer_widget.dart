import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/home/home_viewmodel.dart';
import 'package:resolvair/presentation/widgets/connect_deconnect_button_widget.dart';
import 'package:stacked/stacked.dart';

class DrawerWidget extends ViewModelWidget<HomeViewModel> {
  const DrawerWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, HomeViewModel viewModel) {
    return Drawer(
        backgroundColor: const Color.fromARGB(255, 29, 29, 29),
        child: ListView(children: <Widget>[
          DrawerHeader(
            decoration: const BoxDecoration(
              color: Color.fromARGB(255, 0, 0, 0),
            ),
            child: Text(
              LocaleKeys.app_revolvair_text.tr(),
              style: const TextStyle(
                color: Color.fromARGB(255, 255, 255, 255),
                fontSize: 24,
              ),
            ),
          ),
          ListTile(
            title: Text(
              LocaleKeys.app_test_air_quality.tr(),
              style: const TextStyle(
                color: Colors.white,
              ),
            ),
            onTap: () {
              viewModel.navigateToAirPage();
            },
          ),
          ListTile(
            title: Text(
              LocaleKeys.app_text_drawer_favorite.tr(),
              style: const TextStyle(
                color: Colors.white,
              ),
            ),
            onTap: () {
              if (viewModel.checkConnection()) {
                viewModel.navigateToFavoritePage();
              } else {
                viewModel.navigateToLoginPage();
              }
            },
          ),
          const ConnectDisconnectButtonWidget(),
        ]));
  }
}
