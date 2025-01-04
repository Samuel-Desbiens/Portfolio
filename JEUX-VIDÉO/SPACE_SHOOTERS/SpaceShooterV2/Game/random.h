#pragma once
class Random
{
public:
  Random();
  Random(int seed);
  
  int Next(int minValue, int maxValue);
  int Next(int maxValue);
  double NextDouble();
};

