import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';

abstract class SecureStorageRepositoryInterface{
  Future<Result<Unit, Failure>> storeToken(String token);
  Future<Result<String?, Failure>> retriveToken();
  Future<Result<Unit, Failure>> deleteToken();
}