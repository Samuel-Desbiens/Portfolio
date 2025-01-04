import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:resolvair/domain/entities/ranges.dart';
import 'package:resolvair/generated/locale_keys.g.dart';
import 'package:resolvair/presentation/views/resolvair/revolvair_viewmodel.dart';
import 'package:resolvair/presentation/widgets/details_screen_widget.dart';
import 'package:stacked/stacked.dart';

class RevolvairView extends StatelessWidget {
  final List<Ranges> revolvairRanges;
  const RevolvairView({Key? key, required this.revolvairRanges})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ViewModelBuilder<RevolvairViewModel>.reactive(
      viewModelBuilder: () =>
          RevolvairViewModel(revolvairRanges: revolvairRanges),
      onViewModelReady: (viewModel) => viewModel.initialize(),
      builder: (context, viewModel, child) {
        final ranges = viewModel.revolvairRanges;
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
                      subtitle:
                          Text(LocaleKeys.app_text_range_air.tr(namedArgs: {
                        'min': range.min.toString(),
                        'max': range.max.toString(),
                      })));
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
