// ignore_for_file: hash_and_equals

class Station {
  final int id;
  final String slug;
  final String name;
  final double lat;
  final double long;
  final int activate;
  final int userId;
  final int? commentCount;
  final double? latestMeasureValue;

  Station({
    required this.id,
    required this.slug,
    required this.name,
    required this.lat,
    required this.long,
    required this.activate,
    required this.userId,
    this.commentCount,
    this.latestMeasureValue,
  });

  factory Station.fromJson(Map<String, dynamic> json) {
    return Station(
      id: json['id'],
      slug: json['slug'],
      name: json['name'],
      lat: json['lat'],
      long: json['long'],
      activate: json['activate'],
      userId: json['user_id'],
      commentCount: json['comment_count'],
      latestMeasureValue: (json['latest_measures'].isNotEmpty)
          ? json['latest_measures'][0]['value'].toDouble()
          : 0.0,
    );
  }

  @override
  bool operator ==(Object other) {
    return other is Station &&
        other.id == id &&
        other.name == name &&
        other.slug == slug &&
        other.lat == lat &&
        other.long == long &&
        other.activate == activate &&
        other.userId == userId &&
        other.commentCount == commentCount;
  }
}

class LatestMeasure {
  final int id;
  final String unit;
  final double value;
  final String createdAt;

  LatestMeasure({
    required this.id,
    required this.unit,
    required this.value,
    required this.createdAt,
  });

  factory LatestMeasure.fromJson(Map<String, dynamic> json) {
    return LatestMeasure(
      id: json['id'],
      unit: json['unit'],
      value: json['value'].toDouble(),
      createdAt: json['created_at'],
    );
  }
}
