import 'package:stacked/stacked.dart';
import 'package:osm_nominatim/osm_nominatim.dart';

class SearchViewModel extends BaseViewModel {
  List<Place> locations = [];
  List<double> coordinates = [];

  Future<void> initialize() async {}

  void callOsm(String searchQuery) async {
    final searchResult = await Nominatim.searchByName(
        query: searchQuery,
        limit: 5,
        addressDetails: true,
        extraTags: true,
        nameDetails: true);

    locations = searchResult;

    notifyListeners();
  }
}
