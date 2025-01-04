using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsCape : MonoBehaviour
{
    public Cape droppedObject;
    [SerializeField] List<CapeMold> lootDrops = new List<CapeMold>();

    private CapeMold getDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<CapeMold> possibleItems = new List<CapeMold>();
        foreach (CapeMold item in lootDrops)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            return possibleItems[Random.Range(0, possibleItems.Count)];
        }
        else
        {
            return lootDrops[0];
        }

    }

    public Cape getDroppedObject()
    {
        CapeMold droppedItem = getDroppedItem();
        if (droppedItem != null)
        {
            droppedObject.name = droppedItem.capename;
            droppedObject.GetComponent<SpriteRenderer>().sprite = droppedItem.capeSprite;
            droppedObject.mold = getDroppedItem();
            return droppedObject;
        }
        else { return null; }

    }
}