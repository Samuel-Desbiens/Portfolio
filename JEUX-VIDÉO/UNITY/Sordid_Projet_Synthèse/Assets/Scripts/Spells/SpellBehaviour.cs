using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface SpellBehaviour
{
    public enum SpellElements
    {
        None = 0,
        Fire,
        Water,
        Nature,
        Air
    }
    public float GetDamage();
    public float GetCooldown();
    public SpellElements GetElements();
    public string GetDescription();
}
