// GENERATED CODE - DO NOT MODIFY BY HAND

// **************************************************************************
// StackedNavigatorGenerator
// **************************************************************************

// ignore_for_file: no_leading_underscores_for_library_prefixes
import 'package:flutter/material.dart' as _i10;
import 'package:flutter/material.dart';
import 'package:resolvair/domain/entities/ranges.dart' as _i11;
import 'package:resolvair/presentation/views/air/air_view.dart' as _i3;
import 'package:resolvair/presentation/views/ajoutCommentaire/ajout_commentaire_view.dart'
    as _i8;
import 'package:resolvair/presentation/views/favorite/favorite_view.dart'
    as _i9;
import 'package:resolvair/presentation/views/home/home_view.dart' as _i2;
import 'package:resolvair/presentation/views/login/login_view.dart' as _i6;
import 'package:resolvair/presentation/views/register/register_view.dart'
    as _i7;
import 'package:resolvair/presentation/views/search/search_view.dart' as _i4;
import 'package:resolvair/presentation/views/stationdetails/stationdetails_view.dart'
    as _i5;
import 'package:stacked/stacked.dart' as _i1;
import 'package:stacked_services/stacked_services.dart' as _i12;

class Routes {
  static const homeView = '/home-view';

  static const airView = '/air-view';

  static const searchView = '/search-view';

  static const stationDetailsView = '/station-details-view';

  static const loginView = '/login-view';

  static const registerView = '/register-view';

  static const ajoutCommentaireView = '/ajout-commentaire-view';

  static const favoriteView = '/favorite-view';

  static const all = <String>{
    homeView,
    airView,
    searchView,
    stationDetailsView,
    loginView,
    registerView,
    ajoutCommentaireView,
    favoriteView,
  };
}

class StackedRouter extends _i1.RouterBase {
  final _routes = <_i1.RouteDef>[
    _i1.RouteDef(
      Routes.homeView,
      page: _i2.HomeView,
    ),
    _i1.RouteDef(
      Routes.airView,
      page: _i3.AirView,
    ),
    _i1.RouteDef(
      Routes.searchView,
      page: _i4.SearchView,
    ),
    _i1.RouteDef(
      Routes.stationDetailsView,
      page: _i5.StationDetailsView,
    ),
    _i1.RouteDef(
      Routes.loginView,
      page: _i6.LoginView,
    ),
    _i1.RouteDef(
      Routes.registerView,
      page: _i7.RegisterView,
    ),
    _i1.RouteDef(
      Routes.ajoutCommentaireView,
      page: _i8.AjoutCommentaireView,
    ),
    _i1.RouteDef(
      Routes.favoriteView,
      page: _i9.FavoriteView,
    ),
  ];

  final _pagesMap = <Type, _i1.StackedRouteFactory>{
    _i2.HomeView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i2.HomeView(),
        settings: data,
      );
    },
    _i3.AirView: (data) {
      final args = data.getArgs<AirViewArguments>(nullOk: false);
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) =>
            _i3.AirView(key: args.key, revolvairRanges: args.revolvairRanges),
        settings: data,
      );
    },
    _i4.SearchView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i4.SearchView(),
        settings: data,
      );
    },
    _i5.StationDetailsView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i5.StationDetailsView(),
        settings: data,
      );
    },
    _i6.LoginView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i6.LoginView(),
        settings: data,
      );
    },
    _i7.RegisterView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i7.RegisterView(),
        settings: data,
      );
    },
    _i8.AjoutCommentaireView: (data) {
      final args = data.getArgs<AjoutCommentaireViewArguments>(nullOk: false);
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) =>
            _i8.AjoutCommentaireView(key: args.key, slug: args.slug),
        settings: data,
      );
    },
    _i9.FavoriteView: (data) {
      return _i10.MaterialPageRoute<dynamic>(
        builder: (context) => const _i9.FavoriteView(),
        settings: data,
      );
    },
  };

  @override
  List<_i1.RouteDef> get routes => _routes;

  @override
  Map<Type, _i1.StackedRouteFactory> get pagesMap => _pagesMap;
}

class AirViewArguments {
  const AirViewArguments({
    this.key,
    required this.revolvairRanges,
  });

  final _i10.Key? key;

  final List<_i11.Ranges> revolvairRanges;

  @override
  String toString() {
    return '{"key": "$key", "revolvairRanges": "$revolvairRanges"}';
  }

  @override
  bool operator ==(covariant AirViewArguments other) {
    if (identical(this, other)) return true;
    return other.key == key && other.revolvairRanges == revolvairRanges;
  }

  @override
  int get hashCode {
    return key.hashCode ^ revolvairRanges.hashCode;
  }
}

class AjoutCommentaireViewArguments {
  const AjoutCommentaireViewArguments({
    this.key,
    required this.slug,
  });

  final _i10.Key? key;

  final String slug;

  @override
  String toString() {
    return '{"key": "$key", "slug": "$slug"}';
  }

  @override
  bool operator ==(covariant AjoutCommentaireViewArguments other) {
    if (identical(this, other)) return true;
    return other.key == key && other.slug == slug;
  }

  @override
  int get hashCode {
    return key.hashCode ^ slug.hashCode;
  }
}

extension NavigatorStateExtension on _i12.NavigationService {
  Future<dynamic> navigateToHomeView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.homeView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToAirView({
    _i10.Key? key,
    required List<_i11.Ranges> revolvairRanges,
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  }) async {
    return navigateTo<dynamic>(Routes.airView,
        arguments: AirViewArguments(key: key, revolvairRanges: revolvairRanges),
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToSearchView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.searchView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToStationDetailsView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.stationDetailsView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToLoginView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.loginView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToRegisterView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.registerView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToAjoutCommentaireView({
    _i10.Key? key,
    required String slug,
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  }) async {
    return navigateTo<dynamic>(Routes.ajoutCommentaireView,
        arguments: AjoutCommentaireViewArguments(key: key, slug: slug),
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> navigateToFavoriteView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return navigateTo<dynamic>(Routes.favoriteView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithHomeView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.homeView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithAirView({
    _i10.Key? key,
    required List<_i11.Ranges> revolvairRanges,
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  }) async {
    return replaceWith<dynamic>(Routes.airView,
        arguments: AirViewArguments(key: key, revolvairRanges: revolvairRanges),
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithSearchView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.searchView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithStationDetailsView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.stationDetailsView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithLoginView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.loginView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithRegisterView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.registerView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithAjoutCommentaireView({
    _i10.Key? key,
    required String slug,
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  }) async {
    return replaceWith<dynamic>(Routes.ajoutCommentaireView,
        arguments: AjoutCommentaireViewArguments(key: key, slug: slug),
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }

  Future<dynamic> replaceWithFavoriteView([
    int? routerId,
    bool preventDuplicates = true,
    Map<String, String>? parameters,
    Widget Function(BuildContext, Animation<double>, Animation<double>, Widget)?
        transition,
  ]) async {
    return replaceWith<dynamic>(Routes.favoriteView,
        id: routerId,
        preventDuplicates: preventDuplicates,
        parameters: parameters,
        transition: transition);
  }
}
