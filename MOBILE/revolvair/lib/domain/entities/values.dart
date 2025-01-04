class Values {
  final double value;
  final String unit, date;

  Values({
    required this.value,
    required this.date,
    required this.unit,
  });

  factory Values.fromJson(Map<String, dynamic> json) {
    return Values(
      value:  json['value']?.toDouble(),
      date: json['date'].toString(),
      unit: json['unit'],
    );
  }

  @override
  // ignore: hash_and_equals
  bool operator ==(Object other) {
    return other is Values &&
        other.value == value &&
        other.date == date &&
        other.unit == unit;
  }
}
