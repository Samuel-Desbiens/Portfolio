#include <WebServer.h>
#include <ESPmDNS.h>

class RevolvairWebServer
{
public:
    void Setup();
    void Loop(int pms2p5i,
              String label,
              String color,
              String healthEffect,
              String note,
              String macId,
              String deviceId,
              String SSID,
              String RSSI);

    void handleRoot();
    void handleInfo();
    void handleCss();
    void handlepm2p5();
    void handlelabel();
    void handlecolor();
    void handlehealtheffect();
    void handlenote();
    void handlemacaddress();
    void handledeviceid();
    void handleSSID();
    void handleRSSI();
    void handleNotFound();

protected:
private:
    String GetDataType(String path);
    String pms2p5s;
    String labelAQHI;
    String colorAQHI;
    String healthEffectAQHI;
    String noteAQHI;
    String macAddress;
    String deviceId;
    String SSID;
    String RSSI;
};