import 'dart:io';

class ApiClientRoute {
  static const _androidServerUrl = "https://10.0.2.2:7251/";
  static const _iosServerUrl = "https://localhost:7251/";
  static String apiBaseUrl() =>
      Platform.isAndroid ? _androidServerUrl : _iosServerUrl;
  final String chatPostUrl = "chat/PostMessage";
}
