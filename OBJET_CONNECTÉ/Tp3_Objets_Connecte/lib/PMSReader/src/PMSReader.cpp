#include "PMSReader.h"
#include <PMS.h>
#include <SPI.h>
#include <ESPmDNS.h>

PMS pms(Serial2);
PMS::DATA data;

void PMSReader::Setup()
{
  Serial2.begin(9600);
  this->pmg1 = 0;
  this->pmg2 = 0;
  this->pmg10 = 0;
}

void PMSReader::Loop()
{
  Serial.print("Tring to read...");
  if (pms.readUntil(data, 2))
  {
    Serial.print("PM 1.0 (ug/m3): ");
    this->pmg1 = data.PM_AE_UG_1_0;
    Serial.println(this->pmg1);
    Serial.print("PM 2.5 (ug/m3): ");
    this->pmg2 = data.PM_AE_UG_2_5;
    Serial.println(this->pmg2);
    Serial.print("PM 10.0 (ug/m3): ");
    this->pmg10 = data.PM_AE_UG_10_0;
    Serial.println(this->pmg10);
    Serial.println();
  }
}

int PMSReader::GetData1p0()
{
  return this->pmg1;
}

int PMSReader::GetData2p5()
{
  return this->pmg2;
}

int PMSReader::GetData10p0()
{
  return this->pmg10;
}