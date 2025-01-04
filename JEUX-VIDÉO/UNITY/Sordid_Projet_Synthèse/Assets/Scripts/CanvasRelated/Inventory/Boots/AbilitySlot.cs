using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : Slot
{

    BootsAbility currentAbility;

    [SerializeField] Sprite[] abilitiesSprites;


    // Update is called once per frame
    void Update()
    {
        
    }



    public void UpdateBootSkill(GameObject boots)
    {
        if (boots != null) 
        {
            currentAbility = boots.GetComponent<Boots>().GetAbility();
        }
        else 
        {
            currentAbility = BootsAbility.None;
        }
        DisplaySkill();
    }

    void DisplaySkill()
    {
        switch (currentAbility)
        {
            case BootsAbility.None:
                slotImage = null;
                slotImage.color = hideItemColor;
                break;
            case BootsAbility.Dash:
                slotImage.sprite = abilitiesSprites[0]; break;
            case BootsAbility.TripleJump:
                slotImage.sprite = abilitiesSprites[1]; break;
            case BootsAbility.Teleport:
                slotImage.sprite = abilitiesSprites[2]; break;
        }

        slotImage.color = showItemColor;
    }
}
