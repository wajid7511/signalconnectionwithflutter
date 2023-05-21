import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:signal_r_app/abstractions/chat/chat_methods_name.dart';
import 'package:signal_r_app/abstractions/chat/dto/new_message_dto.dart';
import 'package:signal_r_app/abstractions/chat/i_chat_service.dart';
import 'package:signal_r_app/abstractions/signal_r_hubs/i_chat_hubs.dart';
import 'package:signal_r_app/core/chat_service.dart';
import 'package:signal_r_app/core/signal_r_hubs/hub_service.dart';
import 'package:signal_r_app/pages/chat/chat_page.dart';

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  String allClientsMessage = "No one notified yet!";
  final TextEditingController _controller = TextEditingController(text: '');
  var sizedBox = const SizedBox(
    height: 20,
  );
  late IChatService _chatService;
  late IChatHubService _chatHubService;
  List<NewMessageDto> messages = [];
  @override
  void initState() {
    super.initState();
    _chatService = ChatService();
    _chatHubService = ChatHubService();
    _chatHubService.connectToHub();
    _chatHubService.onNewMessage(
        ChatMethodsName.notifyClients, onMessageRecieved);
    _chatHubService.onNewMessage(
        ChatMethodsName.notifyOtherClients, onOtherMessageRecieved);
  }

  // void _pushMessageToServer() async {
  //   var platformName = Platform.isAndroid ? "Android" : "Iso";
  //   _chatHubService.pushMessageToServer("This message from $platformName");
  // }

  onMessageRecieved(List<dynamic>? newMessage) {
    if (newMessage != null) {
      messages.add(NewMessageDto.fromJson(jsonDecode(newMessage[0])));
      setState(() {});
    } else {
      if (kDebugMode) {
        print("Arguments are null");
      }
    }
  }

  onOtherMessageRecieved(List<dynamic>? newMessage) {
    try {
      if (newMessage != null) {
        messages.add(NewMessageDto.fromJson(jsonDecode(newMessage[0])));
        setState(() {});
      } else {
        if (kDebugMode) {
          print("Arguments are null");
        }
      }
    } catch (ex) {
      if (kDebugMode) {
        print("Unable to parse message$ex");
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        bottomSheet: SizedBox(
          width: MediaQuery.of(context).size.width,
          child: TextField(
            controller: _controller,
            decoration: InputDecoration(
              suffixIcon: IconButton(
                icon: const Icon(Icons.send),
                onPressed: () async {
                  var isSuccess = await _chatService.notifyOthers(
                      _controller.text, _chatHubService.getClientId());
                  if (isSuccess) {
                    messages.add(NewMessageDto(
                        _chatHubService.getClientId() ?? "",
                        _controller.text,
                        DateTime.now().toUtc().toString()));

                    _controller.clear();
                    setState(() {});
                  }
                },
              ),
              border: const OutlineInputBorder(),
              hintText: 'Enter message to your message',
            ),
          ),
        ),
        appBar: AppBar(
          centerTitle: true,
          title: Text(widget.title),
        ),
        body: ChatPage(_chatHubService.getClientId() ?? "", messages),
      ),
    );
  }
}
