import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

class TokenManagerImpl implements TokenManagerInterface {
  String token = '';

  @override
  String getToken(){
      var result = validateToken();
      result.when((success) => unit, (error) => token = '');
      return token;
  }

  @override
  void setToken(String token) {
    this.token = token;
  }

  Result<Unit, Failure> validateToken() {
    try{
      if (token.isNotEmpty && !JwtDecoder.isExpired(token)) {
        return const Success(unit);
      } else {
        return Error(ResponseFailure());
      }
    } catch (error) {
      return Error(ResponseFailure());
    }
    
  }
}
