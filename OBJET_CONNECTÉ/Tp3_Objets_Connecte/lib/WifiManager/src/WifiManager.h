#include <WiFi.h>
#include <WiFiClient.h>
#include <WiFiAP.h>

class WifiManager
{
public:
    void Setup(const char *ssid, const char *key);
    void wifiConnect();
    String getSSID();
    String getRSSI();

protected:
private:
    const char *SSID;
    const char *Pass;
    String MSSID; // Un peu en double mais fait une confirmation par la machine hence M
    String RSSI;
};