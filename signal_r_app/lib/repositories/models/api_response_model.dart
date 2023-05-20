class ApiResponseModel<T> {
  late bool isSuccess;
  late int statusCode;
  late int totalPages;
  late String message;
  T? data;
  Error? error;

  ApiResponseModel(
      this.isSuccess, this.statusCode, this.totalPages, this.message,
      {this.data, this.error});

  ApiResponseModel.fromJson(Map<String, dynamic> json) {
    isSuccess = json['isSuccess'];
    statusCode = json['statusCode'];
    message = json['message'];
    data = json['data'];
    error = json['error'] != null ? Error.fromJson(json['error']) : null;
    totalPages = json['totalPages'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['isSuccess'] = isSuccess;
    data['statusCode'] = statusCode;
    data['message'] = message;
    data['data'] = this.data;
    if (error != null) {
      data['error'] = error!.toJson();
    }
    data['totalPages'] = totalPages;
    return data;
  }
}

class Error {
  late String errorCode;
  late String englishMessage;
  String? urduMessage;
  String? stackTrace;

  Error(this.errorCode, this.englishMessage,
      {this.urduMessage, this.stackTrace});

  Error.fromJson(Map<String, dynamic> json) {
    errorCode = json['errorCode'];
    englishMessage = json['englishMessage'];
    urduMessage = json['urduMessage'];
    stackTrace = json['stackTrace'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['errorCode'] = errorCode;
    data['englishMessage'] = englishMessage;
    data['urduMessage'] = urduMessage;
    data['stackTrace'] = stackTrace;
    return data;
  }
}
