import 'dart:io';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:logging/logging.dart';
import 'package:signal_r_app/pages/home/my_home_page.dart';
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
