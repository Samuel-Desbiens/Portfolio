import 'dart:io';

class EnvVariables {
  // Private constructor
  EnvVariables._();

  // Singleton instance
  static final EnvVariables _instance = EnvVariables._();

  // Factory constructor to get the instance
  factory EnvVariables() {
    return _instance;
  }

  // Load environment variables from a given file path
  loadEnv(String filePath) {
    try {
      final file = File(filePath);
      final lines = file.readAsLinesSync();
      for (final line in lines) {
        final parts = line.split('=');
        if (parts.length == 2) {
          final key = parts[0].trim();
          final value = parts[1].trim();
          _envVars[key] = value;
        }
      }
    } catch (e) {
      // ignore: avoid_print
      print('Erreur lors du chargement du fichier $filePath \n $e');
    }
  }

  // Access environment variables
  final Map<String, String> _envVars = {};

  String getVariable(String key) {
    return _envVars[key] ?? '';
  }
}
