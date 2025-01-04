#include "WifiManager.h"

void WifiManager::Setup(const char *ssid, const char *key)
{
    WiFi.mode(WIFI_STA);

    if (WiFi.status() == WL_CONNECTED)
    {
        WiFi.disconnect();

        while (WiFi.status() != WL_DISCONNECTED)
        {
            // attend la connection wifi
            Serial.println("Waiting Deconnections");
        }
    }

    this->SSID = ssid;
    this->Pass = key;
}

void WifiManager::wifiConnect()
{
    WiFi.begin(this->SSID, this->Pass);
    while (WiFi.status() != WL_CONNECTED)
    {
        // attend la connection wifi
        Serial.println("WaitingConnection");
        Serial.println(WiFi.status());
        delay(500);
    }
    this->MSSID = WiFi.SSID();
    this->RSSI = WiFi.RSSI();
    Serial.println("SSID Found:");
    Serial.println(this->MSSID);
    Serial.println("RSSI Value:");
    Serial.println(this->RSSI);
}

String WifiManager::getSSID()
{
    return this->MSSID;
}

String WifiManager::getRSSI()
{
    return this->RSSI;
}