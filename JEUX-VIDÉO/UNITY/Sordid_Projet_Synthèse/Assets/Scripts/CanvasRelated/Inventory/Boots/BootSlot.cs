using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootSlot : Slot
{
    [SerializeField] AbilitySlot skillSlot;
    // Update is called once per frame
    void Update()
    {
    }

    public override void RemoveItem()
    {
        item = null;
        slotImage.color = hideItemColor;
        slotImage.sprite = null;
        skillSlot.RemoveItem();
    }

    public override void AddItem(GameObject newItem)
    {
        if (!isLocked)
        {
            item = newItem;
            if (item != null)
            {
                slotImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                slotImage.color = showItemColor;
                skillSlot.UpdateBootSkill(newItem);
            }
            else
            {
                slotImage.color = hideItemColor;
            }
        }

    }

    public float GetBootsSpeedValue()
    {
        if (item != null)
        {
            Boots boots = item.GetComponent<Boots>();
            return boots.GetSpeed();
        }
        else { return 0f; }

    }

    public BootsAbility GetAbility()
    {
        if (item != null)
        {
            return item.GetComponent<Boots>().GetAbility();
        }
        else
        {
            return BootsAbility.None;
        }
    }

}
