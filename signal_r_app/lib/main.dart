import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:logging/logging.dart';
import 'package:signal_r_app/repositories/client/api_client.dart';
import 'package:signalr_core/signalr_core.dart';

import 'repositories/http_overrides.dart';

// The location of the SignalR Server.
const androidServerUrl = "https://10.0.2.2:7251/chathub";
const iosServerUrl = "https://localhost:7251/chathub";
// If you want only to log out the message for the higer level hub protocol:
final hubProtLogger = Logger("SignalR - hub");
// If youn want to also to log out transport messages:
final transportProtLogger = Logger("SignalR - transport");

// Creates the connection by using the HubConnectionBuilder.
final hubConnection = HubConnectionBuilder()
    .withUrl(Platform.isAndroid ? androidServerUrl : iosServerUrl)
    .build();

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
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
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
  int _counter = 0;
  String text = "You have pushed the button this many times:";
  void _pushMessageToServer() async {
    try {
      var platformName = Platform.isAndroid ? "Android" : "Iso";
      await hubConnection.invoke('GetMessageFromClient',
          args: ["This message from $platformName"]);
    } catch (ex) {
      if (kDebugMode) {
        print("GetMessageFromClient failed to call $ex");
      }
    }
  }

  _notifyAllUsingApi() async {
    try {
      final ApiClient apiClient = ApiClient();
      var result = await apiClient.sendBoolAsync<bool>("chat");
      if (kDebugMode) {
        print("Response from Api => ${result.message}");
      }
    } catch (ex) {
      if (kDebugMode) {
        print("GetMessageFromClient failed to call $ex");
      }
    }
  }

  @override
  void initState() {
    super.initState();
    connectToHub();
  }

  void connectToHub() async {
    try {
      await hubConnection.start();

      if (kDebugMode) {
        print("hubConnection started......");
      }
      hubConnection.on("NotifyClient", (arguments) {
        if (arguments != null) {
          text = arguments[0];
          _counter++;
          setState(() {});
        } else {
          if (kDebugMode) {
            print("Arguments are null");
          }
        }
      });
    } catch (ex) {
      if (kDebugMode) {
        print("Connection error =>>>>> $ex");
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            TextButton(
              onPressed: () async {
                await _notifyAllUsingApi();
              },
              child: const Text("Notify All"),
            ),
            Text(
              text,
            ),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headline4,
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _pushMessageToServer,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ),
    );
  }
}
