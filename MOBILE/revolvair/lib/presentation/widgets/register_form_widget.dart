import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/data/utils/constant.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/utils/validators.dart';
import 'package:resolvair/presentation/views/register/register_viewmodel.dart';
import 'package:url_launcher/url_launcher.dart';

class RegisterForm extends StatefulWidget {
  final RegisterViewModel viewModel;

  const RegisterForm(this.viewModel, {super.key});

  @override
  State<RegisterForm> createState() => _RegisterFormState();
}

class _RegisterFormState extends State<RegisterForm> {
  final _formKey = GlobalKey<FormState>();
  bool _isChecked = false;
  String urlTos = Constants.TOS_SOURCE;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
        child: Form(
            key: _formKey,
            child: Column(mainAxisSize: MainAxisSize.min, children: <Widget>[
              const Text('Nom'),
              TextFormField(
                  controller: widget.viewModel.controllerNom,
                  validator: (value) => Validators.validateName(value),
                  decoration: InputDecoration(
                      labelText: LocaleKeys.app_text_your_name.tr(),
                      labelStyle: const TextStyle(color: Colors.black),
                      enabledBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ),
                      focusedBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ))),
              Text(LocaleKeys.app_text_courriel.tr()),
              TextFormField(
                  controller: widget.viewModel.controllerEmail,
                  validator: (value) => Validators.validateEmail(value),
                  decoration: InputDecoration(
                      labelText: LocaleKeys.app_text_hint_email.tr(),
                      labelStyle: const TextStyle(color: Colors.black),
                      enabledBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ),
                      focusedBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ))),
              Text(LocaleKeys.app_text_password.tr()),
              TextFormField(
                  controller: widget.viewModel.controllerPass,
                  validator: (value) => Validators.validatePassword(value),
                  decoration: InputDecoration(
                      labelText: LocaleKeys.app_text_password_confirm.tr(),
                      labelStyle: const TextStyle(color: Colors.black),
                      enabledBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ),
                      focusedBorder: const UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.black),
                      ))),
              Row(
                children: <Widget>[
                  Checkbox(
                      value: _isChecked,
                      onChanged: (bool? value) {
                        setState(() {
                          _isChecked = value!;
                        });
                      }),
                  InkWell(
                    child: Text(LocaleKeys.app_text_term_condition.tr()),
                    onTap: () => launchUrl(Uri.parse(urlTos)),
                  )
                ],
              ),
              ElevatedButton(
                onPressed: !_isChecked
                    ? null
                    : () => {
                          if (_formKey.currentState!.validate())
                            {widget.viewModel.register()}
                        },
                style: ElevatedButton.styleFrom(backgroundColor: Colors.black),
                child: Text(LocaleKeys.app_text_sinscrire.tr()),
              )
            ])));
  }
}
