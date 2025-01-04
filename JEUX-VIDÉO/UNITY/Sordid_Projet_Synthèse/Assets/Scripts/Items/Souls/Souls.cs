using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{

    protected PlayerInventory inventory;
    protected string soulName;
    protected string description;
    protected float activeAbilityDuration;
    protected float activeAbilityCooldown;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public virtual void ApplyPassiveAbility()
    {

    }

    public virtual void RemovePassiveAbility()
    {

    }

    public virtual void ApplyActiveAbility()
    {

    }

    public virtual void RemoveActiveAbility()
    {

    }



    public string GetName()
    {
        return soulName;
    }

    public string GetDescription()
    {
        return description;
    }

    public float GetActiveAbilityDuration()
    {
        return activeAbilityDuration;
    }

    public float GetActiveAbilityCooldown()
    {
        return activeAbilityCooldown;
    }
}
