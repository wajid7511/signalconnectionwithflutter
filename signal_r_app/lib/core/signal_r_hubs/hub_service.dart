import 'package:flutter/foundation.dart';
import 'package:signal_r_app/abstractions/signal_r_hubs/i_chat_hubs.dart';
import 'package:signal_r_app/repositories/constants/api_route.dart';
import 'package:signalr_core/signalr_core.dart';

class ChatHubService implements IChatHubService {
// Creates the connection by using the HubConnectionBuilder.
  final hubConnection =
      HubConnectionBuilder().withUrl(ApiClientRoute.chatHubUrl()).build();

  @override
  Future<bool> connectToHub() async {
    try {
      await hubConnection.start();
      if (kDebugMode) {
        print("hubConnection started......");
      }
      return true;
    } catch (ex) {
      if (kDebugMode) {
        print("Connection error =>>>>> $ex");
      }
    }
    return false;
  }

  @override
  void onNewMessage(String methodName, Function(List<dynamic>?) onReceieved) {
    hubConnection.on("NotifyClient", onReceieved);
  }

  @override
  Future<bool> pushMessageToServer(String message) async {
    try {
      await hubConnection.invoke('GetMessageFromClient', args: [message]);
      return true;
    } catch (ex) {
      if (kDebugMode) {
        print("GetMessageFromClient failed to call $ex");
      }
    }
    return false;
  }
}
