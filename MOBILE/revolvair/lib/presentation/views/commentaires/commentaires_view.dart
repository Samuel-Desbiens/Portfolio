import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/commentaires/commentaires_viewmodel.dart';
import 'package:stacked/stacked.dart';

class CommentairesView extends StatelessWidget {
  const CommentairesView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments;
    return ViewModelBuilder<CommentairesViewModel>.reactive(
      viewModelBuilder: () => CommentairesViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(args.toString()),
      builder: (context, viewModel, child) {
        final isLoading = viewModel.isBusy;

        return Stack(children: [
          Container(
            padding: const EdgeInsets.all(10),
            child: ListView.builder(
              itemCount: viewModel.comments.length,
              itemBuilder: (context, index) {
                final comment = viewModel.comments[index];
                return ListTile(
                  leading: CircleAvatar(
                      backgroundImage: NetworkImage(comment.userImage),
                      radius: 24),
                  title: Text(comment.userName),
                  subtitle: Text(comment.text),
                  trailing:
                      Text(LocaleKeys.app_text_comment_date.tr(namedArgs: {
                    'date': comment.daysPast.toString(),
                  })),
                  isThreeLine: true,
                );
              },
            ),
          ),
          Positioned(
              bottom: 16.0,
              right: 16.0,
              child: FloatingActionButton(
                onPressed: () async {
                  viewModel.checkAuthentication();
                },
                child: const Icon(Icons.create),
              )),
          if (isLoading)
            const Center(
              child: CircularProgressIndicator(),
            )
        ]);
      },
    );
  }
}
