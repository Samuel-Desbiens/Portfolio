#include <Arduino.h>
#include <vector>
#include <map>
class AqhiScale
{
public:
    void Setup();
    void OfflineSetup();
    String GetLabel(int pmg);
    String GetColor(int pmg);
    String GetHealthEffect(int pmg);
    String GetNote(int pmg);

protected:
private:
    void ExtractJson();
    const String url = "https://staging.revolvair.org/api/revolvair/aqi/aqhi";
    std::vector<int> minValues;
    std::vector<int> maxValues;
    std::vector<String> labelsValues;
    std::vector<String> colorsValues;
    std::vector<String> healthEffectsValues;
    std::vector<String> noteValues;
};