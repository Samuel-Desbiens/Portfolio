#include "Screen.h"
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SH110X.h>

#define i2c_Address 0x3c
#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET -1    //   QT-PY / XIAO
Adafruit_SH1106G display = Adafruit_SH1106G(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

#define NUMFLAKES 10
#define XPOS 0
#define YPOS 1
#define DELTAY 2

#define LOGO16_GLCD_HEIGHT 16
#define LOGO16_GLCD_WIDTH 16

void Screen::Setup()
{
  delay(250);                       // wait for the OLED to power up
  display.begin(i2c_Address, true); // Address 0x3C default

  display.display();
  delay(2000);
  display.clearDisplay();
}

void Screen::Loop(int pmg, int status)
{

  display.setTextSize(2);
  display.setTextColor(SH110X_WHITE);
  display.setCursor(32, 0);
  display.print(pmg);
  display.print("ug/m3");
  display.setCursor(32, 32);
  display.print("WIFI:");
  if (status == WL_CONNECTED)
  {
    display.print("ON");
  }
  else
  {
    display.print("OFF");
  }
  display.display();
  display.clearDisplay();
}