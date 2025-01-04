#include <Arduino.h>

class RGBLedManager
{
public:
    struct RGBColor {
        uint8_t red;
        uint8_t green;
        uint8_t blue;
    };
    const int RedPin = 12;
    const int GreenPin = 13;
    const int BluePin = 14;

    void Setup();
    void SetRGB(RGBColor rgbColor);
    RGBColor ConvertToRGB(String colorValue);

    
protected:
private:
    
};