using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionSoul : Souls
{
    [SerializeField] float abilityDuration = 20;
    [SerializeField] float cooldown = 30;
    [SerializeField] int temporaryPowerBoost = 10;
    bool hasDecreasedPower = true;
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
        if (abilityTimer.CanDo() && !hasDecreasedPower)
        {
            RemoveActiveAbility();
        }
    }

    public override void ApplyPassiveAbility()
    {
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        inventory.SetIncreasePowerPickup(true);
    }

    public override void RemovePassiveAbility()
    {
        inventory.SetIncreasePowerPickup(false);
    }

    public override void ApplyActiveAbility()
    {
        abilityTimer.Reset();
        inventory.SetIncreasePower(temporaryPowerBoost);
        abilityTimer.SetCooldown(abilityDuration);
        hasDecreasedPower=false;
    }

    public override void RemoveActiveAbility()
    {
        inventory.SetDecreasePower(temporaryPowerBoost);
        hasDecreasedPower = true;
    }
}
