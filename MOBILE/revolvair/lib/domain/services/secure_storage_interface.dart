abstract class SecureStorageInterface {
  Future storeToken(String token);
  Future<String?> retriveToken();
  Future deleteToken();
}
