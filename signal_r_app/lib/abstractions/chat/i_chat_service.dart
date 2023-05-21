abstract class IChatService {
  Future<bool> notifyAll(String message);
  Future<bool> notifyOthers(String message, String? currentClientId);
}
