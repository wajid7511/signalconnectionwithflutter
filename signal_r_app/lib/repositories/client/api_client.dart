import 'dart:convert';
import 'dart:io';
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import '../../abstractions/exceptions/api_processor_exception.dart';
import '../constants/api_route.dart';
import '../interceptors/auth_interceptor.dart';
import '../models/api_response_model.dart';

class ApiClient {
  final Dio _client = Dio(BaseOptions(
      baseUrl: ApiClientRoute.apiBaseUrl(), contentType: 'application/json'));
  ApiClient() {
    _client.interceptors.add(AuthInterceptor());
  }
  Future<ApiResponseModel<T>> sendAsync<T>(String absoluteUrl,
      {Object? body}) async {
    try {
      var httpResponse = await _client.post(absoluteUrl,
          data: body != null ? jsonEncode(body) : null);
      if (httpResponse.statusCode == HttpStatus.ok &&
          httpResponse.data != null) {
        return ApiResponseModel.fromJson(httpResponse.data!);
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

  Future<ApiResponseModel<T>> getAsync<T>(String absoluteUrl,
      {Map<String, dynamic>? queryParameters}) async {
    try {
      var httpResponse =
          await _client.get(absoluteUrl, queryParameters: queryParameters);
      if (httpResponse.statusCode == HttpStatus.ok &&
          httpResponse.data != null) {
        return ApiResponseModel.fromJson(httpResponse.data!);
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
