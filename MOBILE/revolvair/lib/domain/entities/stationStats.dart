// ignore_for_file: hash_and_equals

class StationStats {
  final String lastValue, averageDay, averageWeek, averageMonth, maxDay, minDay, maxWeek, minWeek;

  StationStats(
    {
      required this.lastValue,
      required this.averageDay,
      required this.averageWeek,
      required this.averageMonth,
      required this.maxDay,
      required this.minDay,
      required this.maxWeek,
      required this.minWeek
    }
  );

  static StationStats fromJson(json) {
    return StationStats(
      lastValue: json['lastValue'],
      averageDay: json['averageDay'],
      averageWeek: json['averageWeek'],
      averageMonth: json['averageMonth'],
      maxDay: json['maxDay'],
      minDay: json['minDay'],
      maxWeek: json['maxWeek'],
      minWeek: json['minWeek']
      );
  }

  @override
  bool operator ==(Object other) {
    return other is StationStats &&
        other.lastValue == lastValue &&
        other.averageDay == averageDay &&
        other.averageWeek == averageWeek &&
        other.averageMonth == averageMonth &&
        other.maxDay == maxDay &&
        other.minDay == minDay &&
        other.maxWeek == maxWeek &&
        other.minWeek == minWeek;
  }
}
