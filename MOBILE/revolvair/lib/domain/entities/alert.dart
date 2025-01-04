class Alertes {
  final int id;
  final double value;
  final String description, date;

  Alertes({
    required this.id,
    required this.value,
    required this.date,
    required this.description,
  });

  factory Alertes.fromJson(Map<String, dynamic> json) {
    return Alertes(
      id:  json['id'].toInt(),
      value:  json['value']?.toDouble(),
      date: json['date'],
      description: json['description'],
    );
  }

  @override
  // ignore: hash_and_equals
  bool operator ==(Object other) {
    return other is Alertes &&
        other.value == value &&
        other.date == date &&
        other.description == description;
  }
}
