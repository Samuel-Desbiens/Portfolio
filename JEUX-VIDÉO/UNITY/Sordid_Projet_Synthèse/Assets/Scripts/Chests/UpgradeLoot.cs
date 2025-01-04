using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/UpgradeLoot")]
public class UpgradeLoot : ScriptableObject
{
    public Sprite upgradeSprite;
    public string Upgradename;
    public string description;
    public int dropChance;

    public UpgradeLoot(string name, string description, int dropChance)
    {
        this.name = name;
        this.description = description;
        this.dropChance = dropChance;
    }
}
