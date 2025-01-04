using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Boots")]
public class BootsMold : ScriptableObject
{
    public Sprite bootSprite;
    public string bootname;
    public string description;
    public float moveSpeedBoost;
    public int dropChance;
    public BootsAbility ability;

    public BootsMold(string name, string description,float moveSpeedBoost, int dropChance, BootsAbility givenAbility)
    {
        this.name = name;
        this.description = description;
        this.moveSpeedBoost = moveSpeedBoost;
        this.dropChance = dropChance;
        ability = givenAbility;
    }

    public BootsAbility getBootsAbility()
    {
        return ability;
    }
}
