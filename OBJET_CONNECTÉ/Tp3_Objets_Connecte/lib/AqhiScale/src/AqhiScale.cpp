#include "AqhiScale.h"
#include <thread>
#include <ArduinoJson.h>
#include <WiFi.h>
#include <HTTPClient.h>

void AqhiScale::Setup()
{
  ExtractJson();
}

String AqhiScale::GetLabel(int pmg)
{
  for (int i = 0; i < this->maxValues.size(); i++)
  {
    if (pmg >= this->minValues[i] && pmg < this->maxValues[i])
    {
      return this->labelsValues[i];
    }
  }
  return "NOT_FOUND";
}

String AqhiScale::GetColor(int pmg)
{
  for (int i = 0; i < this->colorsValues.size(); i++)
  {
    if (pmg >= this->minValues[i] && pmg < this->maxValues[i])
    {
      return this->colorsValues[i];
    }
  }
  return "FFFFFF";
}

String AqhiScale::GetHealthEffect(int pmg)
{
  for (int i = 0; i < this->maxValues.size(); i++)
  {
    if (pmg >= this->minValues[i] && pmg < this->maxValues[i])
    {
      return this->healthEffectsValues[i];
    }
  }
  return "NOT_FOUND";
}

String AqhiScale::GetNote(int pmg)
{
  for (int i = 0; i < this->maxValues.size(); i++)
  {
    if (pmg >= this->minValues[i] && pmg < this->maxValues[i])
    {
      return this->noteValues[i];
    }
  }
  return "NOT_FOUND";
}

void AqhiScale::ExtractJson()
{
  HTTPClient http;
  http.begin(this->url);

  int httpResponseCode = http.GET();

  if (httpResponseCode == 200)
  {
    String payload = http.getString();

    // Parse JSON
    DynamicJsonDocument doc(4096);
    DeserializationError error = deserializeJson(doc, payload);

    if (error)
    {
      Serial.print(F("deserializeJson() failed: "));
      Serial.println(error.c_str());
      return;
    }

    JsonArray ranges = doc["ranges"];

    for (JsonObject range : ranges)
    {
      int min = range["min"];
      int max = range["max"];
      String label = range["label"];
      String color = range["color"];
      String healthEffect = range["health_effect"];
      String note = range["note"];

      this->minValues.push_back(min);
      this->maxValues.push_back(max);
      this->labelsValues.push_back(label);
      this->colorsValues.push_back(color);
      this->healthEffectsValues.push_back(healthEffect);
      this->noteValues.push_back(note);
    }
    http.end();
  }
  else
  {
    Serial.println("Erreur de connection à AQHI");
  }
}

void AqhiScale::OfflineSetup(){
  minValues = {0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
  maxValues = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 10000};
  labelsValues = {"1) Faible risque", "2) Faible risque", "3) Faible risque", "4) Risque modéré", "5) Risque modéré", "6) Risque modéré", "7) Risque élevé", "8) Risque élevé", "9) Risque élevé", "10) Risque élevé", "+ de 10) Risque très élevé"};
  colorsValues = {"00ccff", "0099cc", "006699", "ffff00", "ffcc00", "ff9933", "ff6666", "ff0000", "cc0000", "990000", "990000"};
  healthEffectsValues = {"NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA"};
  noteValues = {};
}
