using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUnlocker : MonoBehaviour
{
    public void UnlockSlot()
    {
        InventoryPersistence.instance.GetComponent<Inventory>().UnlockSlot();
    }
}
