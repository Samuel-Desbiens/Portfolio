using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AirDiscBehaviour : MonoBehaviour, SpellBehaviour
{
    private float Damage = 8f;
    private float KnockBackForce = 10f;
    private float Range = 15f;
    private float MovementSpeed = 11f;
    private float Cooldown = 1.5f;
    private float DistanceTraveled = 0f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Air;

    private const string Description = "A spinning disc of air cutting everything in its path";

    private float Angle;

    SoundManager soundManager;

    private Vector3 PlayerPos;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        soundManager = SoundManager.Instance;

    }

    private void OnEnable()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        this.ResetSpell();
        this.Cast();
    }

    private void ResetSpell()
    {
        DistanceTraveled = 0f;
    }

    private void Cast()
    {
        Vector3 FiringPos = transform.position;
        soundManager.PlayAudio(soundManager.airBoomrangClip, transform.position);
        Vector3 MousePosToCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Angle = Mathf.Atan2((MousePosToCam - FiringPos).y, (MousePosToCam - FiringPos).x);
    }

    void Update()
    {
        PlayerPos = GameObject.Find("Player").transform.position;
        if (DistanceTraveled >= Range)
        {
            Angle = Mathf.Atan2((PlayerPos - transform.position).y, (PlayerPos - transform.position).x);

            Vector2 NextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

            transform.Translate(NextPoint * MovementSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 OldPos = transform.position;

            Vector2 NextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

            transform.Translate(NextPoint * MovementSpeed * Time.deltaTime);

            DistanceTraveled += Vector2.Distance(transform.position, OldPos);

            if (DistanceTraveled >= Range)
            {
                //Reset la hitbox pour hit sur le retour
                Collider2D collider = gameObject.GetComponent<CircleCollider2D>();
                collider.enabled = false;
                collider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            if (DistanceTraveled >= Range)
            {
                gameObject.SetActive(false);
            }

        }
        else if (collisionObject.CompareTag("Enemy"))
        {
            float damageAfterBonus = Damage + (Damage / 100) * playerInventory.GetPowerBonus() ;
            collisionObject.GetComponent<Enemy>().TakeDmg(damageAfterBonus, Element);
            Vector3 pos = transform.position;
            Vector3 ColPos = collision.transform.position;
            collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * KnockBackForce);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collisionObject.GetComponent<Health>().TakeDmg(Damage);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float GetDamage()
    {
        return Damage;
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
