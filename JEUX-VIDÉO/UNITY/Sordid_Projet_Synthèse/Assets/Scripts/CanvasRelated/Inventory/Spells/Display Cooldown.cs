using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCooldown : MonoBehaviour
{
    private Image cooldownImage;

    float BaseCooldown = 0.0f; //Take Max Cooldown from Spell
    void Start()
    {
        cooldownImage = GetComponent<Image>();
        cooldownImage.fillAmount = 0.0f;
    }

    public void SetMaxCooldownTime(float time)
    {
        BaseCooldown = time;
    }

    public void UpdateCooldownTime(float time)
    {
        cooldownImage.fillAmount = time / BaseCooldown;
    }
}
