import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository_interface.dart';
import 'package:resolvair/domain/services/token_manager.dart';

class LoadTokenUseCase {
  final TokenManagerInterface tokenManager;
  final SecureStorageRepositoryInterface secureStorage;
  LoadTokenUseCase({required this.tokenManager, required this.secureStorage});

  Future<Result<Unit, Failure>> execute() async {
      bool errorThrown = false;
      Result<String?, Failure> result = await secureStorage.retriveToken();
      String? token;
      result.when(
        (value) => token = value,
        (error) async => errorThrown = true);

      if(token != "" && token != null){
        tokenManager.setToken(token!);
      } else {
        errorThrown = true;
      }

      if(errorThrown){
        return Error(ResponseFailure());
      }
      return const Success(unit);
  }
}
