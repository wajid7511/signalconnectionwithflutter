import 'package:flutter/foundation.dart';
import 'package:signal_r_app/abstractions/chat/i_chat_service.dart';
import 'package:signal_r_app/repositories/client/api_client.dart';

class ChatService implements IChatService {
  @override
  Future<bool> notifyAll(String message) async {
    try {
      final ApiClient apiClient = ApiClient();
      var result = await apiClient
          .sendBoolAsync<bool>("chat", body: {"message": message});
      if (kDebugMode) {
        print("Response from Api => ${result.message}");
      }
      return true;
    } catch (ex) {
      if (kDebugMode) {
        print("GetMessageFromClient failed to call $ex");
      }
    }
    return false;
  }
}
