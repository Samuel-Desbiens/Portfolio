import 'package:resolvair/presentation/views/favorite/favorite_view.dart';
import 'package:resolvair/presentation/views/home/home_view.dart';
import 'package:resolvair/presentation/views/login/login_view.dart';
import 'package:resolvair/presentation/views/register/register_view.dart';
import 'package:resolvair/presentation/views/stationdetails/stationdetails_view.dart';
import 'package:stacked/stacked_annotations.dart';

import '../views/air/air_view.dart';
import '../views/search/search_view.dart';
import 'package:resolvair/presentation/views/AjoutCommentaire/ajout_commentaire_view.dart';

@StackedApp(
  routes: [
    MaterialRoute(page: HomeView),
    MaterialRoute(page: AirView),
    MaterialRoute(page: SearchView),
    MaterialRoute(page: StationDetailsView),
    MaterialRoute(page: LoginView),
    MaterialRoute(page: RegisterView),
    MaterialRoute(page: AjoutCommentaireView),
    MaterialRoute(page: FavoriteView),
  ],
)
class App {}
