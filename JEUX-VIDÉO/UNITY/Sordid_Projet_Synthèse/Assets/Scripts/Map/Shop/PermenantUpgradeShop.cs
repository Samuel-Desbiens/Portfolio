using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermenantUpgradeShop : Shop
{
    protected override void Buy()
    {
        base.Buy();
        Debug.Log("Bought froom permanent shop");
        PermanentUpgradesManager.instance.AddUsedShop(this);
    }
}
