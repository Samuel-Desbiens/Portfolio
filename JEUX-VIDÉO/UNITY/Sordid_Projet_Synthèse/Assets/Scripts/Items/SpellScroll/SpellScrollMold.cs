using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.TextCore;


[CreateAssetMenu(menuName = "My Assets/SpellScroll")]
public class SpellScrollMold : ScriptableObject
{
    public Sprite SpellScrollSprite;
    public GameObject Spell;
    public GameObject UpgradedSpell;
    public int dropChance;
    public Sprite upgradeSprite;
    public SpellScrollMold(Sprite spellScrollSprite, GameObject spell, GameObject upgradedSpell, Sprite upgradeSprite)
    {
        SpellScrollSprite = spellScrollSprite;
        Spell = spell;
        UpgradedSpell = upgradedSpell;
        this.upgradeSprite = upgradeSprite;
    }
}
