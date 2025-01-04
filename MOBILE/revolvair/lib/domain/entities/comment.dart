// ignore_for_file: hash_and_equals

class Comments {
  final int id;
  final String text;
  final String userName;
  final int userId;
  final String userImage;
  final String daysPast;

  Comments({
    required this.id,
    required this.text,
    required this.userName,
    required this.userId,
    required this.userImage,
    required this.daysPast,
  });

  factory Comments.fromJson(Map<String, dynamic> json) {
    return Comments(
        id: json['id'],
        text: json['text'],
        userName: json['name'],
        userId: json['user']['id'],
        userImage: json['user']['avatar'],
        daysPast: DateTime.now()
            .difference(DateTime.parse(json['created_at']))
            .inDays
            .toString());
  }

  @override
  bool operator ==(Object other) {
    return other is Comments &&
        other.id == id &&
        other.text == text &&
        other.userName == userName &&
        other.userId == userId &&
        other.userImage == userImage;
  }
}
