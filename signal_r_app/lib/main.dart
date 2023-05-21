import 'dart:io';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:logging/logging.dart';
import 'package:signal_r_app/abstractions/chat/chat_methods_name.dart';
import 'package:signal_r_app/abstractions/signal_r_hubs/i_chat_hubs.dart';
import 'package:signal_r_app/core/chat_service.dart';
import 'package:signal_r_app/core/signal_r_hubs/hub_service.dart';
import 'abstractions/chat/i_chat_service.dart';
import 'repositories/http_overrides.dart';

// If you want only to log out the message for the higer level hub protocol:
final hubProtLogger = Logger("SignalR - hub");
// If youn want to also to log out transport messages:
final transportProtLogger = Logger("SignalR - transport");

void main() {
  HttpOverrides.global = MyHttpOverrides();
  Logger.root.level = Level.ALL;
// Writes the log messages to the console
  Logger.root.onRecord.listen((LogRecord rec) {
    if (kDebugMode) {
      print('${rec.level.name}: ${rec.time}: ${rec.message}');
    }
  });
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Signal Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Signal R'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  String allClientsMessage = "No one notified yet!";
  String otherClientsMessage = "No one notify you yet!";
  final TextEditingController _controller = TextEditingController(text: '');
  final TextEditingController _otherClientcontroller =
      TextEditingController(text: '');
  var sizedBox = const SizedBox(
    height: 20,
  );
  late IChatService _chatService;
  late IChatHubService _chatHubService;

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

  void _pushMessageToServer() async {
    var platformName = Platform.isAndroid ? "Android" : "Iso";
    _chatHubService.pushMessageToServer("This message from $platformName");
  }

  onMessageRecieved(List<dynamic>? newMessage) {
    if (newMessage != null) {
      allClientsMessage = newMessage[0];
      setState(() {});
    } else {
      if (kDebugMode) {
        print("Arguments are null");
      }
    }
  }

  onOtherMessageRecieved(List<dynamic>? newMessage) {
    if (newMessage != null) {
      otherClientsMessage = newMessage[0];
      setState(() {});
    } else {
      if (kDebugMode) {
        print("Arguments are null");
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  width: MediaQuery.of(context).size.width / 1.2,
                  child: TextField(
                    controller: _controller,
                    decoration: InputDecoration(
                      suffixIcon: IconButton(
                        icon: const Icon(Icons.notifications),
                        onPressed: () async {
                          var isSuccess =
                              await _chatService.notifyAll(_controller.text);
                          if (isSuccess) {
                            _controller.clear();
                          }
                        },
                      ),
                      border: const OutlineInputBorder(),
                      hintText: 'Enter message to notify all',
                    ),
                  ),
                ),
              ],
            ),
            sizedBox,
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                allClientsMessage,
                style: Theme.of(context).textTheme.headline4,
              ),
            ),
            sizedBox,
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  width: MediaQuery.of(context).size.width / 1.2,
                  child: TextField(
                    controller: _otherClientcontroller,
                    decoration: InputDecoration(
                      suffixIcon: IconButton(
                        icon: const Icon(Icons.notifications),
                        onPressed: () async {
                          var isSuccess = await _chatService.notifyOthers(
                              _otherClientcontroller.text,
                              _chatHubService.getClientId());
                          if (isSuccess) {
                            _otherClientcontroller.clear();
                          }
                        },
                      ),
                      border: const OutlineInputBorder(),
                      hintText: 'Enter message to notify others',
                    ),
                  ),
                ),
              ],
            ),
            sizedBox,
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Text(
                otherClientsMessage,
                style: Theme.of(context).textTheme.headline5,
              ),
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _pushMessageToServer,
        tooltip: 'Push new message to server',
        child: const Icon(Icons.add),
      ),
    );
  }
}
