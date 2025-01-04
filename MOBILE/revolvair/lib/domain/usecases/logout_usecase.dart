import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/auth_service.dart';

class GetAuthLogoutUseCase {
  final AuthServiceInterface authService;

  GetAuthLogoutUseCase({required this.authService});

  Future<Result<Unit, Failure>> execute() async {
    final status = await authService.logout();
    return status;
  }
}
