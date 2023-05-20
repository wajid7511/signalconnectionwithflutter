import 'dart:io';
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';

class AuthInterceptor extends Interceptor {
  @override
  void onError(DioError err, ErrorInterceptorHandler handler) {
    if (kDebugMode) {
      print(err.toString());
    }
    super.onError(err, handler);
  }

  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    options.headers.putIfAbsent("content-type", () => "application/json");
    if (kDebugMode) {
      print("Headers added");
    }
    super.onRequest(options, handler);
  }

  @override
  void onResponse(Response response, ResponseInterceptorHandler handler) {
    if (response.statusCode == HttpStatus.unauthorized) {
      if (kDebugMode) {
        print("User is un authorized");
      }
    } else {
      if (kDebugMode) {
        print("User is authorized");
      }
    }
    super.onResponse(response, handler);
  }
}
