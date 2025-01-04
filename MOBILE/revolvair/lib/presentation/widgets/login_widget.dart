import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/utils/validators.dart';
import 'package:resolvair/presentation/views/login/login_viewmodel.dart';

class LoginFormWidget extends StatefulWidget {
  final LoginViewModel viewModel;

  const LoginFormWidget(this.viewModel, {super.key});

  @override
  _LoginFormWidgetState createState() => _LoginFormWidgetState();
}

class _LoginFormWidgetState extends State<LoginFormWidget> {
  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Form(
        key: _formKey,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Image.asset('assets/logo.jpg'),
            Padding(
              padding: const EdgeInsets.all(16.0),
              child: TextFormField(
                controller: widget.viewModel.emailController,
                validator: (value) => Validators.validateEmail(value),
                decoration: InputDecoration(
                  labelText: LocaleKeys.app_text_courriel.tr(),
                  focusedBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.black),
                  ),
                  enabledBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.black),
                  ),
                ),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(16.0),
              child: TextFormField(
                controller: widget.viewModel.passwordController,
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return LocaleKeys.app_text_empty_field.tr();
                  }
                  return null;
                },
                obscureText: true,
                decoration: InputDecoration(
                  labelText: LocaleKeys.app_text_password.tr(),
                  focusedBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.black),
                  ),
                  enabledBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(color: Colors.black),
                  ),
                ),
              ),
            ),
            SizedBox(
              width: 200.0,
              child: ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    widget.viewModel.login();
                  }
                },
                child: Text(LocaleKeys.app_text_login.tr()),
              ),
            ),
            SizedBox(
              width: 200.0,
              child: ElevatedButton(
                onPressed: () {
                  widget.viewModel.navigateToRegisterPage();
                },
                child: Text(LocaleKeys.app_text_no_compte.tr()),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
