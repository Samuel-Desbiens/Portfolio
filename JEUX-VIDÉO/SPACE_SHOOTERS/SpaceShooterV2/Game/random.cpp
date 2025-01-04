#include "stdafx.h"
#include "random.h"
#include <time.h>
#include <stdlib.h>

Random::Random()
{
  srand((unsigned) time(NULL));
}

Random::Random(int seed)
{
  srand(seed);
}


int Random::Next(int minValue, int maxValue)
{
  double pct = (double)rand()/ (double) RAND_MAX;
  return minValue + (int)(pct*(maxValue-minValue));
}

int Random::Next(int maxValue)
{
  return Next(0, maxValue);
}

double Random::NextDouble()
{
  return (double)rand() / (double)RAND_MAX;
}
