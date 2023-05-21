class NewMessageDto {
  late String clientId;
  late String textMessage;
  late String dateTime;
  NewMessageDto(this.clientId, this.textMessage, this.dateTime);
  NewMessageDto.fromJson(Map<String, dynamic> json) {
    textMessage = json['message'];
    clientId = json['clientId'];
    dateTime = json['dateTimeOffset'];
  }
}
