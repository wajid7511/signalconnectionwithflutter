import 'package:flutter/foundation.dart';
import 'package:signal_r_app/abstractions/chat/i_chat_service.dart';
import 'package:signal_r_app/repositories/client/api_client.dart';
import 'package:signal_r_app/repositories/constants/api_route.dart';

class ChatService implements IChatService {
  @override
  Future<bool> notifyAll(String message) async {
    return await sendMessageAsync(ApiClientRoute.chatUrl(),
        body: {"message": message});
  }

  @override
  Future<bool> notifyOthers(String message, String? currentClientId) async {
    return await sendMessageAsync(ApiClientRoute.chatNotifyOthersUrl(),
        body: {"message": message, "clientId": currentClientId});
  }

  Future<bool> sendMessageAsync(String absoluteUrl, {Object? body}) async {
    try {
      final ApiClient apiClient = ApiClient();
      var result = await apiClient.sendAsync<bool>(absoluteUrl, body: body);
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
