import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/domain/failures/custom_failures.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';

class PostCommentUseCase {
  final RevolvairServiceInterface stationApi;

  PostCommentUseCase({required this.stationApi});

  Future<Result<String, Failure>> execute(
      String commentContent, String slug) async {
    final commentStatus = stationApi.postComment(commentContent, slug);
    return commentStatus;
  }
}
