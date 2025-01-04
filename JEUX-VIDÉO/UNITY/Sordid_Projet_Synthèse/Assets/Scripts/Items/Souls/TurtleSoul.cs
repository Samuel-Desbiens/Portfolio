using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurtleSoul : Souls
{
    Health playerHealth;
    [SerializeField] float maxHealthBonus = 50f;
    [SerializeField] float abilityDuration = 20;
    [SerializeField] float cooldown = 30;
    Timer abilityTimer;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        activeAbilityDuration = abilityDuration;
        activeAbilityCooldown = cooldown;
        abilityTimer = new(activeAbilityDuration);
    }

    void Update()
    {
        abilityTimer.Update();
        if (abilityTimer.CanDo())
        {
            RemoveActiveAbility();
        }
    }

    public override void ApplyPassiveAbility()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerHealth.IncreaseMaxHealth(maxHealthBonus);
    }

    public override void RemovePassiveAbility()
    {
        playerHealth.SetIncreaseRegen(false);
        playerHealth.DecreaseMaxHealth(maxHealthBonus);
    }

    public override void ApplyActiveAbility()
    {
        abilityTimer.Reset();
        playerHealth.SetIncreaseRegen(true);
        abilityTimer.SetCooldown(abilityDuration);
    }

    public override void RemoveActiveAbility()
    {
        playerHealth.SetIncreaseRegen(false);
    }
}