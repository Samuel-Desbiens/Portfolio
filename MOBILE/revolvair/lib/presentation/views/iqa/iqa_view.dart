import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/iqa/iqa_viewmodel.dart';
import 'package:resolvair/presentation/widgets/details_screen_widget.dart';
import 'package:stacked/stacked.dart';

class IqaView extends StatelessWidget {
  const IqaView({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<IqaViewModel>.reactive(
      viewModelBuilder: () => IqaViewModel(),
      onViewModelReady: (viewModel) => viewModel.initialize(),
      builder: (context, viewModel, child) {
        final ranges = viewModel.ranges;
        final isLoading = viewModel.isBusy;

        return Scaffold(
          body: Stack(
            children: [
              ListView.builder(
                itemCount: ranges.length,
                itemBuilder: (context, index) {
                  final range = ranges[index];
                  return ListTile(
                    onTap: () {
                      Navigator.of(context).push(
                        MaterialPageRoute(
                          builder: (context) {
                            return DetailScreen(range: range);
                          },
                        ),
                      );
                    },
                    leading: Hero(
                      tag: 'colorCircle_${range.label}',
                      child: Container(
                        width: 30,
                        height: 24,
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          color: range.getColorFromHex(),
                        ),
                      ),
                    ),
                    title: Text(range.label),
                    subtitle: Text(LocaleKeys.app_text_range_air.tr(namedArgs: {
                      'min': range.min.toString(),
                      'max': range.max.toString(),
                    })),
                  );
                },
              ),
              if (isLoading)
                const Center(
                  child: CircularProgressIndicator(),
                ),
            ],
          ),
        );
      },
    );
  }
}
