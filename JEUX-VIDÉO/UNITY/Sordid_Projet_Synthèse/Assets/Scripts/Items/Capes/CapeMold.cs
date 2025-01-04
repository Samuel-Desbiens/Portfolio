using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Cape")]
public class CapeMold : ScriptableObject
{
    public Sprite capeSprite;
    public string capename;
    public string description;
    public float healthBoost;
    public float cooldownBoost;
    public float fireBoost;
    public float waterBoost;
    public float airBoost;
    public float natureBoost;
    
    public int dropChance;

    public CapeMold(string name, string description, float healthBonus,float cooldownBonus, float fireBonus, float waterBonus, float airBonus, float natureBonus, int dropChance)
    {
        capename = name;
        this.description = description;
        this.dropChance = dropChance;
        healthBoost = healthBonus;
        cooldownBoost = cooldownBonus;
        fireBoost = fireBonus;
        waterBoost = waterBonus;
        airBoost = airBonus;
        natureBoost = natureBonus;
    }
}
