import 'dart:convert';
import 'package:easy_localization/easy_localization.dart';
import 'package:http/http.dart' as http;
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/utils/constant.dart';
import 'package:resolvair/domain/failures/auth_failure.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/auth_service.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/generated/locale_keys.g.dart';

class AuthService implements AuthServiceInterface {
  final http.Client httpClient;
  final TokenManagerInterface tokenManager;

  AuthService({required this.httpClient, required this.tokenManager});

  @override
  Future<Result<String, Failure>> getLoginToken(
      String email, String password) async {
    const apiUrl = Constants.URL_LOGIN;
    try {
      final headers = <String, String>{
        'Content-Type': 'application/json',
      };
      final response = await httpClient.post(
        Uri.parse(apiUrl),
        headers: headers,
        body: json.encode({
          'email': email,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        final responseData = json.decode(response.body);
        final accessToken = responseData['token'];
        if (accessToken != null) {
          return Result.success(accessToken);
        } else {
          return Error(
              AuthError(message: LocaleKeys.failures_error_auth_login.tr()));
        }
      } else {
        return Error(LoginError());
      }
    } catch (e) {
      return Error(
          AuthError(message: LocaleKeys.failures_error_auth_login.tr()));
    }
  }

  @override
  Future<Result<String, Failure>> register(
      String name, String email, String password) async {
    try {
      const registerURL = Constants.URL_REGISTER;
      final headers = <String, String>{
        'accept': 'application/json',
        'Content-Type': 'application/json',
      };
      final response = await httpClient.post(
        Uri.parse(registerURL),
        headers: headers,
        body: json.encode({'name': name, 'email': email, 'password': password}),
      );
      if (response.statusCode == 201) {
        final String token = json.decode(response.body)['token'];
        return Success(token);
      } else {
        return Error(
            AuthError(message: LocaleKeys.failures_error_auth_login.tr()));
      }
    } catch (error) {
      return Error(AuthError());
    }
  }

  @override
  Future<Result<Unit, Failure>> logout() async {
    final token = tokenManager.getToken();
    const apiUrl = Constants.URL_LOGOUT;
    try {
      final headers = <String, String>{
        'Authorization': 'Bearer $token',
      };

      final response = await httpClient.post(
        Uri.parse(apiUrl),
        headers: headers,
      );

      if (response.statusCode == 200) {
        tokenManager.setToken("");
        return const Success(unit);
      } else {
        return Error(LogoutError());
      }
    } catch (e) {
      return Error(LogoutError());
    }
  }
}
