using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpellScript : MonoBehaviour, SpellBehaviour
{
    public float GetCooldown()
    {
        return 0;
    }

    public float GetDamage()
    {
        return 0;
    }

    public SpellBehaviour.SpellElements GetElements()
    {
        return SpellBehaviour.SpellElements.None;
    }

    public string GetDescription()
    {
        return "Ya Rien Icitte Fait de l'air";
    }
}
