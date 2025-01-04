import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository_interface.dart';
import 'package:resolvair/domain/services/token_manager.dart';

class StoreTokenUseCase {
  final TokenManagerInterface tokenManager;
  final SecureStorageRepositoryInterface secureStorage;
  StoreTokenUseCase({required this.tokenManager, required this.secureStorage});

  Future<Result<Unit, Failure>> execute() async {
    String token = tokenManager.getToken();
    if (token.isNotEmpty) {
      return await secureStorage.storeToken(token);
    } else if (token == "") {
      return await secureStorage.deleteToken();
    }
    return Error(ResponseFailure());
  }
}
