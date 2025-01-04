using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsBoots : MonoBehaviour
{
    public Boots droppedObject;
    [SerializeField] List<BootsMold> bootDrops = new List<BootsMold>();

    private BootsMold getDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<BootsMold> possibleItems = new List<BootsMold>();
        foreach (BootsMold item in bootDrops)
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
            return bootDrops[0];
        }
        
    }

    public Boots getDroppedObject()
    {
        BootsMold droppedItem = getDroppedItem();
        if (droppedItem != null)
        {
            droppedObject.name = droppedItem.bootname;
            droppedObject.gameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.bootSprite;
            droppedObject.mold = droppedItem;
            return droppedObject;
        }
        else { return null; }
       
    }
}
