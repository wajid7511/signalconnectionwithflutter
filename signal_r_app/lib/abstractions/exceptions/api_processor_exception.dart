class ApiProcessorException implements Exception {
  Exception? innerException;
  late String message;
  ApiProcessorException(this.message, {this.innerException});
}
