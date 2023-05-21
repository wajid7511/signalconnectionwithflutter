class NewPushChatMessage {
  late String message;
  late String? clientId;

  NewPushChatMessage(this.message, this.clientId);

  NewPushChatMessage.fromJson(Map<String, dynamic> json) {
    message = json['message'];
    clientId = json['clientId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['message'] = message;
    data['clientId'] = clientId;
    return data;
  }
}
