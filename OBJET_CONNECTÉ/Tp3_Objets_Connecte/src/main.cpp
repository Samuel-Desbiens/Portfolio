#include <Arduino.h>
#include <Esp.h> //Necessaire device id

#include <SPI.h>
#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>
#include "config.h"
#include "WifiManager.h"
#include "RevolvairWebServer.h"
#include "FlashFileReader.h"
#include "PMSReader.h"
#include "AqhiScale.h"
#include "RGBLedManager.h"
#include "Screen.h"

WifiManager wifiManager;
RevolvairWebServer revolvairWebServer;
FlashFileReader flashFileReader;
PMSReader pmsreader;
AqhiScale aqhiscale;
RGBLedManager ledManager;
Screen screen;

int pms1;
int pms2p5;
int pms10;

String macId;
String deviceId;

void setup()
{
  Serial.begin(115200);
  Serial1.begin(9600);
  deviceId = String(ESP.getEfuseMac());
  macId = String(WiFi.macAddress());
  Serial.println("ESP-32 Dev Module Device Id Is:");
  Serial.println(deviceId);
  Serial.println("ESP-32 Dev Module Mac Address Is:");
  Serial.println(macId);

  wifiManager.Setup(ssid, password);
  revolvairWebServer.Setup();
  flashFileReader.Setup();
  pmsreader.Setup();
  aqhiscale.Setup();
  ledManager.Setup();
  screen.Setup();
}

void loop()
{
  if (WiFi.status() != WL_CONNECTED)
  {
    screen.Loop(0, WiFi.status());
    wifiManager.wifiConnect();
    aqhiscale.Setup();
  }
  else
  {
    pmsreader.Loop();
    pms1 = pmsreader.GetData1p0();
    pms2p5 = pmsreader.GetData2p5();
    pms10 = pmsreader.GetData10p0();
    revolvairWebServer.Loop(pms2p5, 
      aqhiscale.GetLabel(pms2p5), 
      aqhiscale.GetColor(pms2p5), 
      aqhiscale.GetHealthEffect(pms2p5), 
      aqhiscale.GetNote(pms2p5), 
      macId, deviceId, 
      wifiManager.getSSID(), 
      wifiManager.getRSSI());
    screen.Loop(pms2p5, WiFi.status());
    ledManager.SetRGB(ledManager.ConvertToRGB(aqhiscale.GetColor(pms2p5)));
  }
}