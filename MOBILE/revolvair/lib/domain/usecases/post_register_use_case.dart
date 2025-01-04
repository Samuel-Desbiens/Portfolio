import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/auth_service.dart';

class PostRegisterUseCase {
  final AuthServiceInterface apiService;

  PostRegisterUseCase({required this.apiService});

  Future<Result<String, Failure>> execute(
      String name, String email, String password) async {
    var token = await apiService.register(name, email, password);
    return token;
  }
}
