import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/ajoutCommentaire/ajout_commentaire_viewmodel.dart';

import 'package:stacked/stacked.dart';

class AjoutCommentaireView extends StatelessWidget {
  final String slug;
  const AjoutCommentaireView({Key? key, required this.slug}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<AjoutCommentaireViewModel>.reactive(
      viewModelBuilder: () => AjoutCommentaireViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(slug),
      builder: (context, viewModel, child) {
        return Scaffold(
          appBar: AppBar(
              backgroundColor: Colors.black,
              title: Text(LocaleKeys.app_comment_page_title.tr())),
          body: ListView(
            children: [
              Container(
                  padding: const EdgeInsets.all(20),
                  child: SearchBar(
                    onChanged: (value) => viewModel.commentText = value,
                    hintText: LocaleKeys.app_hint_comments.tr(),
                  )),
              Positioned(
                bottom: 16.0,
                right: 16.0,
                child: FloatingActionButton(
                  onPressed: () async {
                    viewModel.pushComment();
                    Navigator.pop(context);
                  },
                  child: const Icon(Icons.done),
                ),
              )
            ],
          ),
        );
      },
    );
  }
}
