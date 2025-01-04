using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerAbilities : MonoBehaviour
{
    ProjectileManager pm;
    //Inventory
    Inventory playerInventory;
    //Cooldown Dans L'ordre du tableau Cooldowns  (Pour �viter d'appeler des variables pour rien)
    /*
     * Sort de Base (Left Click)
     * Sort 1 (Right Click)
     * Sort 2 (Q)
     * Sort 3 (E)
     * Sort Ultime (R)
     */
    private const int UTLIMATE_SPELL_SLOT_POSITION = 4;
    float[] cooldowns = new float[] { 0, 0, 0, 0, 0 };
    void Start()
    {

        pm = FindFirstObjectByType<ProjectileManager>();
        playerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }



    private void Update()
    {
        CheckFiring();
    }
    private void CheckFiring()
    {
        UltimateSlot ultimateSlot = playerInventory.GetUltimateSlot();
        if (ultimateSlot.GetInput().triggered && ultimateSlot.OffCooldown() && ultimateSlot.GetItem() != null)
        {
            UltimateActivation(ultimateSlot.GetItem(), ultimateSlot);
        }
        for (int i = 0; i < playerInventory.GetHotbarSlotsLength(); i++)
        {
            HotbarSlot slot = playerInventory.GetHotbarSlot(i);

            if (slot.GetInput().triggered && slot.OffCooldown() && slot.GetItem() != null)
            {
                string spellName = slot.GetItem().GetComponent<SpellScrolls>().GetSpell().name;
                ProjectileActivation(spellName, slot);
            }
        }
    }

    private void ProjectileActivation(string projectileName, HotbarSlot slot)
    {
        GameObject proj = pm.GetSpell(projectileName);
        if (projectileName == "LeafShields" || projectileName == "LeafShieldsUpgrade")
        {
            proj.GetComponent<LeafShieldBehaviour>().SetInput(slot.GetInputName());
        }
        else if (projectileName == "IceSlash" || projectileName == "IceSlashUpgrade")
        {
            proj.GetComponent<IceSlashBehaviour>().SetInput(slot.GetInputName());
        }
        proj.transform.position = transform.GetChild(0).position;
        proj.gameObject.SetActive(true);
        slot.SetCooldown(proj.gameObject.GetComponent<SpellBehaviour>().GetCooldown());
    }

    private void UltimateActivation(GameObject item, HotbarSlot slot)
    {
        Souls soul = item.GetComponent<Souls>();
        soul.ApplyActiveAbility();
        slot.SetCooldown(soul.GetActiveAbilityCooldown());

    }
}
