import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:signal_r_app/abstractions/chat/dto/new_message_dto.dart';

class ConversationList extends StatefulWidget {
  final NewMessageDto newMessageDto;
  final String currentClientId;
  const ConversationList({
    super.key,
    required this.currentClientId,
    required this.newMessageDto,
  });
  @override
  // ignore: library_private_types_in_public_api
  _ConversationListState createState() => _ConversationListState();
}

class _ConversationListState extends State<ConversationList> {
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(left: 16, right: 16, top: 10, bottom: 20),
      child: Column(
        crossAxisAlignment:
            widget.newMessageDto.clientId == widget.currentClientId
                ? CrossAxisAlignment.end
                : CrossAxisAlignment.start,
        children: <Widget>[
          Text(
            widget.newMessageDto.textMessage,
            style: const TextStyle(fontSize: 16),
          ),
          const SizedBox(
            height: 6,
          ),
          Text(
            DateFormat("dd-MM-yyyy hh:mm:ss")
                .format(DateTime.parse(widget.newMessageDto.dateTime)),
            style: const TextStyle(fontSize: 12, fontWeight: FontWeight.bold),
          ),
        ],
      ),
    );
  }
}
