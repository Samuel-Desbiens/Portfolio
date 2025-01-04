import 'package:resolvair/presentation/app/app_locator.dart';
import 'package:stacked/stacked.dart';
import 'package:stacked_services/stacked_services.dart';
import 'package:resolvair/domain/usecases/post_comment_use_case.dart';

class AjoutCommentaireViewModel extends BaseViewModel {
  final dialogService = locator<DialogService>();
  final PostCommentUseCase postCommentUseCase = locator<PostCommentUseCase>();
  late String slug;
  late String commentText;

  

  Future<void> initialize(String param) async {
    slug = param;
  }

  void pushComment() {
    postCommentUseCase.execute(commentText, slug);
  }
}
