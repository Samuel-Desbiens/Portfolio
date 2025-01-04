using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsSpell : MonoBehaviour
{
    public SpellScrolls droppedObject;
    [SerializeField] List<SpellScrollMold> lootDrops = new List<SpellScrollMold>();

    private SpellScrollMold getDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<SpellScrollMold> possibleItems = new List<SpellScrollMold>();
        foreach (SpellScrollMold item in lootDrops)
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

    public SpellScrolls getDroppedObject()
    {
        SpellScrollMold droppedItem = getDroppedItem();
        if (droppedItem != null)
        {
            droppedObject.name = droppedItem.name;
            droppedObject.gameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.SpellScrollSprite;
            droppedObject.mold = droppedItem;
            return droppedObject;
        }
        else { return null; }

    }
}
