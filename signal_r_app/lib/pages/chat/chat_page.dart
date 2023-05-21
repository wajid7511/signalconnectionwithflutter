import 'package:flutter/material.dart';
import 'package:signal_r_app/abstractions/chat/dto/new_message_dto.dart';
import 'package:signal_r_app/pages/widgets/chat/conversation_list.dart';

class ChatPage extends StatefulWidget {
  final String currentClientId;
  final List<NewMessageDto> messages;
  const ChatPage(this.currentClientId, this.messages, {super.key});

  @override
  // ignore: library_private_types_in_public_api
  _ChatPageState createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final ScrollController _scrollController = ScrollController();
  @override
  void initState() {
    super.initState();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        reverse: true,
        physics: const BouncingScrollPhysics(),
        child: Column(
          children: [
            const Text(
              "Conversations",
              style: TextStyle(fontSize: 32, fontWeight: FontWeight.bold),
            ),
            Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                widget.messages.isEmpty
                    ? const Text(
                        "Conversion not started yet",
                        style: TextStyle(
                            fontSize: 24, fontWeight: FontWeight.w600),
                      )
                    : Padding(
                        padding: const EdgeInsets.only(bottom: 40.0),
                        child: ListView.builder(
                          scrollDirection: Axis.vertical,
                          controller: _scrollController,
                          itemCount: widget.messages.length,
                          shrinkWrap: true,
                          padding: const EdgeInsets.only(top: 16),
                          physics: const NeverScrollableScrollPhysics(),
                          itemBuilder: (context, index) {
                            return ConversationList(
                              currentClientId: widget.currentClientId,
                              newMessageDto: widget.messages[index],
                            );
                          },
                        ),
                      ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
