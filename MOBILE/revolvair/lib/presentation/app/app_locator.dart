import 'package:get_it/get_it.dart';
import 'package:resolvair/data/api_auth_impl.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';
import 'package:resolvair/data/locator_service_impl.dart';
import 'package:resolvair/data/wrapper/geolocator_wrapper.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository_interface.dart';
import 'package:resolvair/domain/repositories/secure_storage_repository.dart';
import 'package:resolvair/domain/repositories/token_manager_impl.dart';
import 'package:resolvair/domain/services/auth_service.dart';
import 'package:resolvair/domain/services/location_service.dart';
import 'package:resolvair/domain/services/revolvair_service.dart';
import 'package:resolvair/domain/services/stationdetails_service.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/get_air_quality_use_case.dart';
import 'package:resolvair/domain/usecases/logout_usecase.dart';
import 'package:resolvair/domain/usecases/login_usecase.dart';
import 'package:resolvair/domain/usecases/get_favorite_station_use_case.dart';
import 'package:resolvair/domain/usecases/get_location_use_case.dart';
import 'package:resolvair/data/station_detail_service_impl.dart';
import 'package:resolvair/domain/usecases/get_alertes_usecase.dart';
import 'package:resolvair/domain/usecases/get_station_comments_usecase.dart';
import 'package:resolvair/domain/usecases/get_station_use_case.dart';
import 'package:resolvair/domain/usecases/get_station_value_24H_usecase.dart';
import 'package:resolvair/domain/usecases/get_statistiques_usecase.dart';
import 'package:resolvair/domain/usecases/load_token_use_case.dart';
import 'package:resolvair/domain/usecases/post_favorite_use_case.dart';
import 'package:resolvair/domain/usecases/post_register_use_case.dart';
import 'package:resolvair/domain/usecases/store_token_use_case.dart';
import 'package:stacked_services/stacked_services.dart';
import 'package:http/http.dart' as http;
import 'package:resolvair/domain/usecases/post_comment_use_case.dart';

GetIt locator = GetIt.instance;

class AppSetup {
  static Future<void> setupLocator() async {
    _registerServices();
    _registerUseCases();
    _registerWrapper();
  }

  static void _registerServices() {
    locator.registerLazySingleton(() => NavigationService());
    locator.registerLazySingleton(() => DialogService());
    locator.registerLazySingleton(() => http.Client());
    locator
        .registerLazySingleton<TokenManagerInterface>(() => TokenManagerImpl());
    locator.registerLazySingleton<SecureStorageRepositoryInterface>(
        () => SecureStorageRepository());
    locator.registerLazySingleton<LocationServiceInterface>(
        () => LocatorService(wrapper: GeoLocatorWrapper()));
    locator.registerLazySingleton<StationDetailsServiceInterface>(
        () => StationDetailsService(httpClient: locator<http.Client>()));
    locator.registerLazySingleton<AuthServiceInterface>(() => AuthService(
        httpClient: locator<http.Client>(),
        tokenManager: locator<TokenManagerInterface>()));
    locator.registerLazySingleton<RevolvairServiceInterface>(() =>
        RevolvairService(
            httpClient: locator<http.Client>(),
            tokenManager: locator<TokenManagerInterface>()));
  }

  static void _registerUseCases() {
    locator.registerLazySingleton<GetStationValue24HUsecase>(() =>
        GetStationValue24HUsecase(
            apiRest: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<GetStationUseCase>(
        () => GetStationUseCase(apiRest: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<GetAirQualityUseCase>(() =>
        GetAirQualityUseCase(apiRest: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<PostFavoriteUseCase>(() =>
        PostFavoriteUseCase(apiRest: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<GetLocationUseCase>(() =>
        GetLocationUseCase(apiLocation: locator<LocationServiceInterface>()));
    locator.registerLazySingleton<GetStatistiqueUseCase>(() =>
        GetStatistiqueUseCase(
            apiRest: locator<StationDetailsServiceInterface>()));
    locator.registerLazySingleton<GetAlertesUseCase>(() =>
        GetAlertesUseCase(apiRest: locator<StationDetailsServiceInterface>()));
    locator.registerLazySingleton<GetAuthUseCase>(() => GetAuthUseCase(
        authService: locator<AuthServiceInterface>(),
        tokenManager: locator<TokenManagerInterface>()));
    locator.registerLazySingleton<GetAuthLogoutUseCase>(() =>
        GetAuthLogoutUseCase(authService: locator<AuthServiceInterface>()));
    locator.registerLazySingleton<GetFavoriteStationUseCase>(() =>
        GetFavoriteStationUseCase(
            stationApi: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<PostCommentUseCase>(() =>
        PostCommentUseCase(stationApi: locator<RevolvairServiceInterface>()));
    locator.registerLazySingleton<LoadTokenUseCase>(() => LoadTokenUseCase(
        tokenManager: locator<TokenManagerInterface>(),
        secureStorage: locator<SecureStorageRepositoryInterface>()));
    locator.registerLazySingleton<StoreTokenUseCase>(() => StoreTokenUseCase(
        tokenManager: locator<TokenManagerInterface>(),
        secureStorage: locator<SecureStorageRepositoryInterface>()));
    locator.registerLazySingleton<GetStationCommentsUseCase>(() =>
        GetStationCommentsUseCase(
            detailApi: locator<StationDetailsServiceInterface>()));
    locator.registerLazySingleton<PostRegisterUseCase>(
        () => PostRegisterUseCase(apiService: locator<AuthServiceInterface>()));
  }

  static void _registerWrapper() {
    locator.registerLazySingleton(() => GeoLocatorWrapper());
  }
}
