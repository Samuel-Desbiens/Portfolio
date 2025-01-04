import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/auth_service.dart';
import 'package:resolvair/domain/services/token_manager.dart';

class GetAuthUseCase {
  final AuthServiceInterface authService;
  final TokenManagerInterface tokenManager;

  GetAuthUseCase({required this.authService, required this.tokenManager});

  Future<Result<String, Failure>> execute(String email, String password) async {
    final result = await authService.getLoginToken(email, password);
    result.when((success) => tokenManager.setToken(success), (error) => result);

    return result;
  }
}
