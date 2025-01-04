import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/air/air_viewmodel.dart';
import 'package:resolvair/presentation/views/aqhi/aqhi_view.dart';
import 'package:resolvair/presentation/views/iqa/iqa_view.dart';
import 'package:resolvair/presentation/views/resolvair/revolvair_view.dart';
import 'package:stacked/stacked.dart';

class AirView extends StatelessWidget {
  final List<Ranges> revolvairRanges;
  const AirView({Key? key, required this.revolvairRanges}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder.reactive(
      viewModelBuilder: () => AirViewModel(revolvairRanges: revolvairRanges),
      builder: (context, viewModel, child) => DefaultTabController(
        length: 3,
        child: Scaffold(
          appBar: AppBar(
            backgroundColor: Colors.black,
            title: Text(LocaleKeys.app_test_air_quality.tr()), // Removed const
            bottom: TabBar(
              tabs: [
                Tab(text: LocaleKeys.app_revolvair_text.tr()),
                Tab(text: LocaleKeys.app_aqhi_text.tr()),
                Tab(text: LocaleKeys.app_iqa_text.tr()),
              ],
            ),
          ),
          body: TabBarView(
            children: [
              Center(
                child: RevolvairView(
                  revolvairRanges: viewModel.revolvairRanges,
                ),
              ),
              const Center(
                child: AqhiView(),
              ),
              const Center(
                child: IqaView(),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
