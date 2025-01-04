using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class AirSlashBehaviour : MonoBehaviour, SpellBehaviour
{
    private float Damage = 5;
    private float Range = 12;
    private float Cooldown = 0.4f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Air;

    private const string Description = "A gust of wind with medium range";

    private const float KnockBackForce = 25;

    private float MovementSpeed = 15f;
    private float RemainingRange = 0;

    private float Angle;
    private Vector3 FiringPos;
    SoundManager soundManager;
    private AirSlashRotation Rotation;
    private PlayerInventory playerInventory;


    private void Awake()
    {
        Rotation = transform.GetChild(0).GetComponent<AirSlashRotation>();
        soundManager = SoundManager.Instance;

    }

    private void OnEnable()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        this.Cast();
    }



    private void Cast()
    {
        FiringPos = transform.position;

        Rotation.SetRotation(FiringPos);

        Angle = Mathf.Atan2((Camera.main.ScreenToWorldPoint(Input.mousePosition) - FiringPos).y, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - FiringPos).x);

        RemainingRange = Range;

        soundManager.PlayAudio(soundManager.airSoundClip, transform.position);
    }

    void Update()
    {
        Vector3 OldPos = transform.position;

        Vector2 nextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

        transform.Translate(nextPoint * MovementSpeed * Time.deltaTime);

        float DistanceTraveled = Vector3.Distance(OldPos, transform.position);

        RemainingRange -= DistanceTraveled;

        if (RemainingRange <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        float damageAfterBonus = Damage + (Damage / 100) * playerInventory.GetPowerBonus();

        if (collisionObject.CompareTag("Enemy"))
        {
            collisionObject.GetComponent<Enemy>().TakeDmg(damageAfterBonus, Element);
            Vector3 pos = transform.position;
            Vector3 ColPos = collision.transform.position;
            collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * KnockBackForce);
            gameObject.SetActive(false);
        }
        else if (collisionObject.CompareTag("Boss"))
        {
            collisionObject.GetComponent<Health>().TakeDmg(damageAfterBonus);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float GetCooldown()
    {
        return this.Cooldown;
    }

    public float GetDamage()
    {
        return this.Damage;
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
