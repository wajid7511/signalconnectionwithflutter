abstract class IChatHubService {
  String? getClientId();
  Future<bool> connectToHub();
  onNewMessage(String methodName, Function(List<dynamic>?) onReceieved);
  Future<bool> pushMessageToServer(String message);
}
