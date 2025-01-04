import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';

abstract class AuthServiceInterface {
  Future<Result<String, Failure>> getLoginToken(String email, String password);
  Future<Result<String, Failure>> register(
      String name, String email, String password);
  Future<Result<Unit, Failure>> logout();
}
