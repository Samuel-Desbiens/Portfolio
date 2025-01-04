#include "RevolvairWebServer.h"
#include "FlashFileReader.h"

WebServer server(80);
FlashFileReader ffr;

// HANDLE PAGES

void RevolvairWebServer::handleRoot()
{
  server.send(200, GetDataType(server.uri()), ffr.LoadFromSpiff(server.uri()));
}

void RevolvairWebServer::handleInfo()
{
  server.send(200, GetDataType(server.uri()), ffr.LoadFromSpiff(server.uri()));
}

void RevolvairWebServer::handleCss()
{
  server.send(200, GetDataType(server.uri()), ffr.LoadFromSpiff(server.uri()));
}
// HANDLE CALLS

void RevolvairWebServer::handlepm2p5()
{
  server.send(200, "text/plane", this->pms2p5s);
}

void RevolvairWebServer::handlelabel()
{
  server.send(200, "text/plane", this->labelAQHI);
}

void RevolvairWebServer::handlecolor()
{
  server.send(200, "text/plane", this->colorAQHI);
}

void RevolvairWebServer::handlehealtheffect()
{
  server.send(200, "text/plane", this->healthEffectAQHI);
}

void RevolvairWebServer::handlenote()
{
  server.send(200, "text/plane", this->noteAQHI);
}

void RevolvairWebServer::handlemacaddress()
{
  server.send(200, "text/plane", this->macAddress);
}

void RevolvairWebServer::handledeviceid()
{
  server.send(200, "text/plane", this->deviceId);
}

void RevolvairWebServer::handleSSID()
{
  server.send(200, "text/plane", this->SSID);
}

void RevolvairWebServer::handleRSSI()
{
  server.send(200, "text/plane", this->RSSI);
}

// NOT FOUND
void RevolvairWebServer::handleNotFound()
{
  if (ffr.LoadFromSpiff(server.uri()))
    return;
  String message = "File Not Found\n\n";
  message += "URI: ";
  message += server.uri();
  message += "\nMethod: ";
  message += (server.method() == HTTP_GET) ? "GET" : "POST";
  message += "\nArguments: ";
  message += server.args();
  message += "\n";
  for (uint8_t i = 0; i < server.args(); i++)
  {
    message += " NAME:" + server.argName(i) + "\n VALUE:" + server.arg(i) + "\n";
  }
  server.send(404, "text/plain", message);
  Serial.println(message);
}

// OTHER
void RevolvairWebServer::Setup()
{
  server.on("/", HTTP_GET, std::bind(&RevolvairWebServer::handleRoot, this));
  server.on("/info.html", HTTP_GET, std::bind(&RevolvairWebServer::handleInfo, this));
  server.on("/style.css", HTTP_GET, std::bind(&RevolvairWebServer::handleCss, this));

  server.on("/handlepm2p5", HTTP_GET, std::bind(&RevolvairWebServer::handlepm2p5, this));
  server.on("/handlelabel", HTTP_GET, std::bind(&RevolvairWebServer::handlelabel, this));
  server.on("/handlecolor", HTTP_GET, std::bind(&RevolvairWebServer::handlecolor, this));
  server.on("/handlehealtheffect", HTTP_GET, std::bind(&RevolvairWebServer::handlehealtheffect, this));
  server.on("/handlenote", HTTP_GET, std::bind(&RevolvairWebServer::handlenote, this));
  server.on("/handlemacaddress", HTTP_GET, std::bind(&RevolvairWebServer::handlemacaddress, this));
  server.on("/handledeviceid", HTTP_GET, std::bind(&RevolvairWebServer::handledeviceid, this));
  server.on("/handleSSID", HTTP_GET, std::bind(&RevolvairWebServer::handleSSID, this));
  server.on("/handleRSSI", HTTP_GET, std::bind(&RevolvairWebServer::handleRSSI, this));
  server.onNotFound(std::bind(&RevolvairWebServer::handleNotFound, this));
  server.begin();
}

void RevolvairWebServer::Loop(int pms2p5i, String label, String color, String healthEffect, String note, String macId, String deviceId, String SSID, String RSSI)
{
  this->pms2p5s = String(pms2p5i);
  this->labelAQHI = label;
  this->colorAQHI = color;
  this->healthEffectAQHI = healthEffect;
  this->noteAQHI = note;
  this->macAddress = macId;
  this->deviceId = deviceId;
  this->SSID = SSID;
  this->RSSI = RSSI;
  server.handleClient();
}

String RevolvairWebServer::GetDataType(String path)
{
  String dataType = "text/plain";
  if (path.endsWith("/"))
    path += "index.html";

  if (path.endsWith(".src"))
    path = path.substring(0, path.lastIndexOf("."));
  else if (path.endsWith(".html"))
    dataType = "text/html";
  else if (path.endsWith(".htm"))
    dataType = "text/html";
  else if (path.endsWith(".css"))
    dataType = "text/css";
  else if (path.endsWith(".js"))
    dataType = "application/javascript";
  else if (path.endsWith(".png"))
    dataType = "image/png";
  else if (path.endsWith(".gif"))
    dataType = "image/gif";
  else if (path.endsWith(".jpg"))
    dataType = "image/jpeg";
  else if (path.endsWith(".ico"))
    dataType = "image/x-icon";
  else if (path.endsWith(".xml"))
    dataType = "text/xml";
  else if (path.endsWith(".pdf"))
    dataType = "application/pdf";
  else if (path.endsWith(".zip"))
    dataType = "application/zip";

  if (server.hasArg("download"))
    dataType = "application/octet-stream";

  return dataType;
}
