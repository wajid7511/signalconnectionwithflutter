import 'dart:convert';
import 'dart:io';
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import '../../exceptions/api_processor_exception.dart';
import '../constants/api_route.dart';
import '../interceptors/auth_interceptor.dart';

class ApiClient {
  final Dio _client = Dio(BaseOptions(
      baseUrl: ApiClientRoute.apiBaseUrl(), contentType: 'application/json'));

  Future<Object> sendBoolAsync<T>(String absoluteUrl, {Object? body}) async {
    try {
      _client.interceptors.add(AuthInterceptor());
      var httpResponse = await _client.post<T>(absoluteUrl,
          data: body != null ? jsonEncode(body) : null);
      if (httpResponse.statusCode == HttpStatus.ok &&
          httpResponse.data != null) {
        return httpResponse.data!;
      }
      throw ApiProcessorException(
          "Failed to call api  status code ${httpResponse.statusCode}");
    } catch (ex) {
      if (kDebugMode) {
        print(ex.toString());
      }
      throw ApiProcessorException(ex.toString());
    }
  }
}
