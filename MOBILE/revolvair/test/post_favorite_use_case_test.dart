import 'package:http/http.dart' as http;
import 'package:mocktail/mocktail.dart';
import 'package:multiple_result/multiple_result.dart';
import 'package:resolvair/data/revolvair_service_impl.dart';
import 'package:resolvair/domain/failures/response_failure.dart';
import 'package:resolvair/domain/services/token_manager.dart';
import 'package:resolvair/domain/usecases/post_favorite_use_case.dart';
import 'package:test/test.dart';

class MockHttpClient extends Mock implements http.Client {}

class MockTokenManager extends Mock implements TokenManagerInterface {}

class MockRevolvairService extends Mock implements RevolvairService {}

class FakeUri extends Fake implements Uri {}

Future<void> main() async {
  late PostFavoriteUseCase postFavorite;
  late MockRevolvairService apiRest;

  setUpAll(() async {
    apiRest = MockRevolvairService();
    postFavorite = PostFavoriteUseCase(apiRest: apiRest);
  });

  test('ajout_station_au_favorit_return_success', () async {
    when(() =>
            apiRest.addFavoriteStation(slug: 'levis-parc-georges-maranda-frm'))
        .thenAnswer((_) async => const Success(unit));

    var result =
        await postFavorite.execute(slug: 'levis-parc-georges-maranda-frm');

    expect(result.tryGetSuccess(), unit);
  });

  test('ajout_station_au_favorit_return_error', () async {
    when(() =>
            apiRest.addFavoriteStation(slug: 'levis-parc-georges-maranda-frm'))
        .thenAnswer((_) async => Error(ResponseFailure()));

    var result =
        await postFavorite.execute(slug: 'levis-parc-georges-maranda-frm');

    expect(result.tryGetError(), isA<ResponseFailure>());
  });
}
