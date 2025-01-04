using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UltimateSlot : HotbarSlot
{
    public void SetSoul(GameObject soul)
    {
        item = soul;
        slotImage.sprite = soul.GetComponent<SpriteRenderer>().sprite;
        slotImage.color = showItemColor;
    }

    public void RemoveSoul()
    {
        slotImage.sprite = null;
        slotImage.color = hideItemColor;
    }

    public void ActivateSoul()
    {
        if(item != null && cooldownTimer.CanDo())
        {
            Souls soulscript = item.GetComponent<Souls>();
            soulscript.ApplyActiveAbility();
            SetCooldown(soulscript.GetActiveAbilityCooldown());
        }
    }
}
