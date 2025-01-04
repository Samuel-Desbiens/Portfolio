using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public const float invincibilityTime = 0.2f;
    // Start is called before the first frame update
    [SerializeField] UnityEvent death;
    [SerializeField] UnityEvent loseHP;
    [SerializeField] private float maxHp;
    [SerializeField] private float healthPoints;
    bool canTakeDmg = true;
    bool increaseHealthRegen = false;

    private Color hitColor = new(1, 0.5f, 0.5f, 0.5f);

    private void OnEnable()
    {
        healthPoints = maxHp;
    }

    private void Start()
    {
        healthPoints = maxHp;
    }
    public bool TakeDmg(float amount)
    {
        if (canTakeDmg)
        {
            healthPoints -= amount;
            StartCoroutine(FlashRed());
            if (healthPoints <= 0)
            {
                death.Invoke();
                return true;
            }
            loseHP.Invoke();
        }
        return false;
    }
    public void Heal(float amount)
    {
        if (increaseHealthRegen)
        {
            amount += amount * 0.5f;
        }

        if (healthPoints + amount > maxHp)
        {
            healthPoints = maxHp;
        }
        else
        {
            healthPoints += amount;
        }
    }

    public void SetIncreaseRegen(bool value)
    {
        increaseHealthRegen=value;
    }
    public float GetHPRatio()
    {
        return (healthPoints / maxHp);
    }

    public void IncreaseMaxHealth(float increaseAmount)
    {
        if(increaseAmount > 0)
        {
            maxHp += increaseAmount;
        }
    }

    public void DecreaseMaxHealth(float decreaseAmount)
    {
        if(decreaseAmount > 0)
        {
            maxHp -= decreaseAmount;
            if (healthPoints > maxHp)
            {
                healthPoints = maxHp;
            }
        }
    }

    private IEnumerator FlashRed()
    {
        canTakeDmg = false;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new(1, 1, 1, 1);
        canTakeDmg = true;
    }

    public void ResetGO()
    {
        healthPoints = maxHp;
    }

}
