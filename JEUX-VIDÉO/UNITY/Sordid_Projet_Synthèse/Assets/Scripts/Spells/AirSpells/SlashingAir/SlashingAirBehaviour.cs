using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SlashingAirBehaviour : MonoBehaviour, SpellBehaviour
{
    private float Damage = 5;
    private float Range = 1.5f;
    private float Cooldown = 0.3f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Air;

    private const string Description = "A short burst of air cutting close to you";

    private const float KnockBackForce = 15;

    private float MovementSpeed = 10f;
    private float RemainingRange = 0;

    private float Angle;
    private Vector3 FiringPos;

    private const float Scalling = 1.05f;

    private SlashingAirRotation Rotation;
    private PlayerInventory playerInventory;
    SoundManager soundManager;
    private Vector3 BaseSize;

    private void Awake()
    {
        Rotation = transform.GetChild(0).GetComponent<SlashingAirRotation>();

        BaseSize = transform.localScale;
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
        transform.localScale = BaseSize;
        RemainingRange = Range;
    }

    private void Cast()
    {
        FiringPos = transform.position;

        Rotation.SetRotation(FiringPos);

        Vector3 MouseToCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Angle = Mathf.Atan2((MouseToCamPos - FiringPos).y, (MouseToCamPos - FiringPos).x);
        soundManager.PlayAudio(soundManager.airSoundClip, transform.position);
        RemainingRange = Range;
    }

    void Update()
    {
        transform.localScale *= Scalling;

        Vector3 OldPos = transform.position;

        Vector2 NextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

        transform.Translate(NextPoint * MovementSpeed * Time.deltaTime);

        float DistanceTraveled = Vector3.Distance(OldPos, transform.position);

        RemainingRange -= DistanceTraveled;

        if (RemainingRange <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        float damageAfterBonus = Damage + (Damage / 100) * playerInventory.GetPowerBonus();

        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().TakeDmg(damageAfterBonus, Element);
            Vector3 pos = transform.position;
            Vector3 ColPos = collider.transform.position;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * KnockBackForce);
        }
        else if (collider.tag == "Boss")
        {
            collider.gameObject.GetComponent<Health>().TakeDmg(damageAfterBonus);
        }
    }

    public float GetCooldown()
    {
        return this.Cooldown;
    }

    public float GetDamage()
    {
        return this.Damage + (Damage / 100) * playerInventory.GetPowerBonus(); ;
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
