import 'package:resolvair/domain/entities/ranges.dart';
import 'package:stacked/stacked.dart';

class RevolvairViewModel extends BaseViewModel {
  final List<Ranges> revolvairRanges;
  RevolvairViewModel({required this.revolvairRanges});

  Future<void> initialize() async {}
}
