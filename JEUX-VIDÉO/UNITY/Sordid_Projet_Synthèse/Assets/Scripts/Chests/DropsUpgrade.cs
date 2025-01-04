using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsUpgrade : MonoBehaviour
{
    public GameObject droppedObject;
    [SerializeField] List<UpgradeLoot> lootDrops = new List<UpgradeLoot>();

    private UpgradeLoot getDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<UpgradeLoot> possibleItems = new List<UpgradeLoot>();
        foreach (UpgradeLoot item in lootDrops)
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

    public GameObject getDroppedObject()
    {
        UpgradeLoot droppedItem = getDroppedItem();
        if (droppedItem != null)
        {
            droppedObject.name = droppedItem.Upgradename;
            droppedObject.GetComponent<SpriteRenderer>().sprite = droppedItem.upgradeSprite;
            return droppedObject;
        }
        else { return null; }
       
    }
}
