using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpellScrolls : MonoBehaviour, IItemInfo
{
    public SpellScrollMold mold;
    private GameObject spell;
    private GameObject upgradedSpell;
    private SpriteRenderer spriteRen;
    Sprite upgradeSprite;

    private bool Upgraded = false;

    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        spriteRen.sprite = mold.SpellScrollSprite;
        spell = mold.Spell;
        upgradedSpell = mold.UpgradedSpell;
        upgradeSprite = mold.upgradeSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().TryAddItem(gameObject);

        }
    }

    public GameObject GetSpell()
    {
        if (Upgraded)
        {
            return upgradedSpell;
        }
        else
        {
            return spell;
        }
    }

    public string GetName()
    {
        if (Upgraded)
        {
            return upgradedSpell.name;
        }
        else
        {
            return spell.name;
        }
    }

    public void Upgrade()
    {
        spriteRen.sprite = upgradeSprite;
        Upgraded = true;
    }

    public bool GetUpgraded()
    {
        return Upgraded;
    }
    public string GetItemInfo()
    {
        return spell.GetComponent<SpellBehaviour>().GetDescription();
    }
}
