using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapeSlot : Slot
{
    float[] noStats = { 0, 0, 0, 0, 0, 0 };
    void Update()
    {

    }

    public float[] GetCapeStats()
    {
        if (item != null)
        {
            Cape cape = item.GetComponent<Cape>();
            float[] capeStats = { cape.GetHealthBoost(), cape.GetCooldownBoost(), cape.GetFireBoost(), cape.GetWaterBoost(), cape.GetAirBoost(), cape.GetNatureBoost() };
            return capeStats;
        }
        else
        {
            return noStats;
        }

    }

}
