import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:stacked/stacked.dart';
import 'package:resolvair/presentation/views/login/login_viewmodel.dart';
import 'package:resolvair/presentation/widgets/login_widget.dart';

class LoginView extends StatelessWidget {
  const LoginView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<LoginViewModel>.reactive(
        viewModelBuilder: () => LoginViewModel(),
        onViewModelReady: (viewModel) => viewModel.initialize(),
        builder: (context, viewModel, child) {
          return Scaffold(
            appBar: AppBar(
              backgroundColor: Colors.black,
              title: Text(LocaleKeys.app_text_login.tr()),
            ),
            body: Center(
              child: LoginFormWidget(viewModel),
            ),
          );
        });
  }
}
