#include <iostream>
#include <SPIFFS.h>
#include <WebServer.h>
#include "FS.h"

class FlashFileReader
{
public:
    void Setup();
    String LoadFromSpiff(String path);

protected:
private:
};