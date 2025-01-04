import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository_interface.dart';

class SecureStorageRepository implements SecureStorageRepositoryInterface {
  static final _storage = const FlutterSecureStorage();
  static const _keyToken = 'jsonWebToken';

  SecureStorageRepository();

  @override
  Future<Result<Unit, Failure>> storeToken(String token) async {
    try{
      await _storage.write(key: _keyToken, value: token);
      return const Success(unit);
    } catch(error) {
      return Error(ResponseFailure());
    }
  }
  

  @override
  Future<Result<String?, Failure>> retriveToken() async {
    try{
      String? token = await _storage.read(key: _keyToken);
      return Success(token);
    } catch (error) {
      return Error(ResponseFailure());
    }
    
  }

  @override
  Future<Result<Unit, Failure>> deleteToken() async {
    try{
      await _storage.delete(key: _keyToken);
      return const Success(unit);
    } catch(error) {
      return Error(ResponseFailure());
    }
  }
  
  
}
