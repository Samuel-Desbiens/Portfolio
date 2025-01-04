using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPermanentUpgrade : MonoBehaviour
{
    public void BuyPermanentSlot()
    {
        InventoryPersistence.instance.GetComponentInChildren<Inventory>().UnlockSlot();
    }
}
