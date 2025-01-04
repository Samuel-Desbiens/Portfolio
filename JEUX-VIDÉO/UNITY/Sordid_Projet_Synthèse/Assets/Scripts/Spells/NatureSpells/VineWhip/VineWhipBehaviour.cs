using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class VineWhipBehaviour : MonoBehaviour, SpellBehaviour
{

    private float Damage = 14;
    private float Cooldown = 0.3f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Nature;

    private const string Description = "A whip made of vine, this spell top the charts each year for the most inappropriately used";

    private const float KnockBackForce = 50;

    private float ActualMovementSpeed;
    private const float RotationalMovementSpeed = 10;

    private float BaseRotation;

    Vector3 FiringPos;

    private const float Differential = 0.990f;

    private Transform CastPosition;

    private SpriteRenderer Sprite;

    SoundManager soundManager;


    private void Awake()
    {
        CastPosition = GameObject.Find("Player").transform.GetChild(0);
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        soundManager = SoundManager.Instance;
    }


    private void OnEnable()
    {
        this.ResetSpell();
        this.Cast();
    }

    private void ResetSpell()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    private void Cast()
    {
        FiringPos = transform.position;
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < FiringPos.x)
        {
            Sprite.flipX = true;
            ActualMovementSpeed = RotationalMovementSpeed;
        }
        else
        {
            Sprite.flipX = false;
            ActualMovementSpeed = -RotationalMovementSpeed;
        }
        BaseRotation = transform.rotation.z;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + (Sprite.bounds.size.x / 2), pos.z);
        soundManager.PlayAudio(soundManager.whipClip, transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 45 * Time.deltaTime * ActualMovementSpeed);

        if (BaseRotation + Differential <= transform.rotation.z || BaseRotation - Differential >= transform.rotation.z)
        {
            gameObject.SetActive(false);
        }

        transform.position = new Vector3(CastPosition.position.x, CastPosition.position.y, CastPosition.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderObject = collider.gameObject;
        if (colliderObject.CompareTag("Enemy"))
        {
            colliderObject.GetComponent<Enemy>().TakeDmg(Damage, Element);
            colliderObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y) * KnockBackForce);
        }
        else if (colliderObject.CompareTag("Boss"))
        {
            colliderObject.GetComponent<Health>().TakeDmg(Damage);
        }
    }

    public float GetCooldown()
    {
        return Cooldown;
    }

    public float GetDamage()
    {
        return Damage;
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
