import 'package:flutter/material.dart';

class Ranges {
  final int max;
  final int min;
  final String label;
  final String color;
  final String healthEffect;
  final String note;

  Ranges({
    required this.max,
    required this.min,
    required this.label,
    required this.color,
    required this.healthEffect,
    required this.note,
  });

  Color getColorFromHex() {
    var newColor = color;
    if (color.startsWith('#')) {
      newColor = color.substring(1);
    }
    final intColor = int.parse(newColor, radix: 16);
    return Color(intColor).withOpacity(1.0);
  }

  factory Ranges.fromJson(Map<String, dynamic> json) {
    return Ranges(
      min: json['min'],
      max: json['max'],
      label: json['label'],
      color: json['color'],
      healthEffect: json['health_effect'],
      note: json['note'],
    );
  }

  @override
  // ignore: hash_and_equals
  bool operator ==(Object other) {
    return other is Ranges &&
        other.min == min &&
        other.max == max &&
        other.label == label &&
        other.color == color &&
        other.healthEffect == healthEffect &&
        other.note == note;
  }
}
