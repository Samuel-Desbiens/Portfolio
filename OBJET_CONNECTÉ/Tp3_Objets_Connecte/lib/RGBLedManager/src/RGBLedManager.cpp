#include "RGBLedManager.h"
#include <Arduino.h>
#include <ESPmDNS.h>

const int RedPin = 12;
const int GreenPin = 13;
const int BluePin = 14;

void RGBLedManager::Setup()
{
  pinMode(RedPin,OUTPUT);
  pinMode(GreenPin,OUTPUT);
  pinMode(BluePin,OUTPUT);
}

RGBLedManager::RGBColor RGBLedManager::ConvertToRGB(String colorValue) {
    // Convert hex string to RGB values
    unsigned int colorHex = (unsigned int)strtol(colorValue.c_str(), NULL, 16);

    RGBColor rgbColor;
    rgbColor.red = (colorHex >> 16) & 0xFF;
    rgbColor.green = (colorHex >> 8) & 0xFF;
    rgbColor.blue = colorHex & 0xFF;

    return rgbColor;
  }

void RGBLedManager::SetRGB(RGBColor rgbColor) {
    // Set the RGB values to the LED
    analogWrite(RedPin, rgbColor.red);
    analogWrite(GreenPin, rgbColor.green);
    analogWrite(BluePin, rgbColor.blue);

    delay(1000);
  }