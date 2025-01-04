// ignore_for_file: hash_and_equals

class StationValue {
  final int id;
  final num value;

  StationValue({required this.id, required this.value});

  factory StationValue.fromJson(Map<String, dynamic> json) {
    return StationValue(id: json['station_id'], value: json['value']);
  }

  @override
  bool operator ==(Object other) {
    return other is StationValue && other.id == id && other.value == value;
  }
}
