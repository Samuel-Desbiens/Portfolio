#include "FlashFileReader.h"

void FlashFileReader::Setup()
{
    SPIFFS.begin();
}

String FlashFileReader::LoadFromSpiff(String path)
{
    if (path.endsWith("/"))
    {
        path += "index.html";
    }

    File dataFile = SPIFFS.open(path.c_str(), "r");

    String ret = dataFile.readString();

    dataFile.close();

    return ret;
}