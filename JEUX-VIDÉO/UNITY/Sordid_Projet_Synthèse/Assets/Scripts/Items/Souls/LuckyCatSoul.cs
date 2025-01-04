using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class LuckyCatSoul : Souls
{
    [SerializeField] float abilityDuration = 20;
    [SerializeField] float cooldown = 30;
    Timer abilityTimer;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        activeAbilityDuration = abilityDuration;
        activeAbilityCooldown = cooldown;
        abilityTimer = new(activeAbilityCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        abilityTimer.Update();
        if (abilityTimer.CanDo())
        {
            abilityTimer.Reset();
            RemoveActiveAbility();
        }
    }

    public override void ApplyPassiveAbility()
    {
        //Boost Chest spawn
    }

    public override void RemovePassiveAbility()
    {
        //Revert ChestSpawn
    }

    public override void ApplyActiveAbility()
    {
        if (abilityTimer.CanDo())
        {
            inventory.SetIncreaseCoinPickup(true);
            abilityTimer.SetCooldown(abilityDuration);
        }

    }

    public override void RemoveActiveAbility()
    {
        inventory.SetIncreaseCoinPickup(false);
    }
}