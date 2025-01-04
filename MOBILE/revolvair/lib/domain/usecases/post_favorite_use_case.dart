import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class PostFavoriteUseCase {
  final RevolvairServiceInterface apiRest;

  PostFavoriteUseCase({required this.apiRest});

  Future<Result<Unit, Failure>> execute({required String slug}) async {
    bool errorThrown = false;
    Result<Unit, Failure> result = await apiRest.addFavoriteStation(slug: slug);
      result.when((success) => unit, (error) => errorThrown = true);
    if(errorThrown){
      return Error(ResponseFailure());
    }
    return const Success(unit);
  }
}
