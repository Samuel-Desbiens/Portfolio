using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FireBoltBehaviour : MonoBehaviour, SpellBehaviour
{
    private Rigidbody2D RB;

    private const float Damage = 13;
    private const float Range = 3f;
    private const float Cooldown = 1;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Fire;

    SoundManager soundManager;

    private const string Description = "The first fire spell of any wizards! Causes more than 100 house fire every month";
    private PlayerInventory playerInventory;



    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        soundManager = SoundManager.Instance;

    }

    private void OnEnable()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        this.Cast();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        float damageAfterBonus = Damage + (Damage / 100) * playerInventory.GetPowerBonus();

        GameObject colliderObject = collider.gameObject;

        if (!colliderObject.CompareTag("Player"))
        {
            if (colliderObject.CompareTag("Enemy"))
            {
                colliderObject.GetComponent<Enemy>().TakeDmg(damageAfterBonus, Element);
            }
            else if (colliderObject.CompareTag("Boss"))
            {
                colliderObject.GetComponent<Health>().TakeDmg(damageAfterBonus);
            }

            gameObject.SetActive(false);
        }


    }

    private void Cast()
    {
        Vector2 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RB.AddForce(Direction * Range, ForceMode2D.Impulse);
        soundManager.PlayAudio(soundManager.fireBoltClip, transform.position);
    }

    public float GetDamage()
    {
        return Damage + (Damage / 100) * playerInventory.GetPowerBonus();
    }
    public float GetCooldown()
    {
        return Cooldown;
    }

    public SpellBehaviour.SpellElements GetElements()
    {
        return Element;
    }

    public string GetDescription()
    {
        return Description;
    }
}
