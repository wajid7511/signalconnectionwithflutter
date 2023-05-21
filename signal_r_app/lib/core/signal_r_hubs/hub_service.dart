import 'package:flutter/foundation.dart';
import 'package:signal_r_app/abstractions/chat/chat_methods_name.dart';
import 'package:signal_r_app/abstractions/chat/dto/new_push_chat_message.dart';
import 'package:signal_r_app/abstractions/signal_r_hubs/i_chat_hubs.dart';
import 'package:signal_r_app/repositories/constants/api_route.dart';
import 'package:signalr_core/signalr_core.dart';

class ChatHubService implements IChatHubService {
// Creates the connection by using the HubConnectionBuilder.
  final hubConnection =
      HubConnectionBuilder().withUrl(ApiClientRoute.chatHubUrl()).build();

  @override
  Future<bool> connectToHub() async {
    return _connectServer();
  }

  @override
  void onNewMessage(String methodName, Function(List<dynamic>?) onReceieved) {
    hubConnection.on(methodName, onReceieved);
  }

  @override
  Future<bool> pushMessageToServer(String message) async {
    try {
      if (hubConnection.state == HubConnectionState.disconnected) {
        if (!(await _connectServer())) {
          if (kDebugMode) {
            print("Unable to connect to hub ");
          }
        }
      }
      var object = NewPushChatMessage(message, hubConnection.connectionId);
      await hubConnection
          .invoke(ChatMethodsName.newMessageToServer, args: [object.toJson()]);
      return true;
    } catch (ex) {
      if (kDebugMode) {
        print("GetMessageFromClient failed to call $ex");
      }
    }
    return false;
  }

  Future<bool> _connectServer() async {
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
  String? getClientId() {
    return hubConnection.connectionId;
  }
}
