using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class IceSlashBehaviour : MonoBehaviour, SpellBehaviour
{
  float CooldownBetweenSameCast = 0.25f;
  float SameCastOpportunity = 0;
  float DespawnTimer = 0;

  Vector3 FiringPos;
  Vector3 TargetPos;

  private const float KnockBackForce = 75;

  private float Damage = 17;
  private float Range = 5;
  private float MovementSpeed = 0.05f;
  private float Cooldown = 6;
  private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Water;

  private const string Description = "Creates an ice sword ready to serve!";

  private Transform CasterPosition;

  private GameObject Player;

  SoundManager soundManager;
  bool hasMadeSpawnSound = false;


  private IceSlashRotationAndCollision Rotation;

  private PlayerInputActions pia;
  private InputAction slotInput;


  private void Awake()
  {
    Rotation = transform.GetChild(0).GetComponent<IceSlashRotationAndCollision>();
    soundManager = SoundManager.Instance;
  }

  private void OnEnable()
  {
    Player = GameObject.Find("Player");
    ResetSpell();
    Cast();
    if (slotInput != null) slotInput.Enable();
  }

  private void OnDisable()
  {
    if (slotInput != null) slotInput.Disable();
  }

  public void SetInput(string inputName)
  {
    pia ??= new PlayerInputActions();
    slotInput = pia.FindAction(inputName);
  }

  private void ResetSpell()
  {
    DespawnTimer = Cooldown;
    SameCastOpportunity = CooldownBetweenSameCast;
    hasMadeSpawnSound = false;
  }

  private void Cast()
  {
    if (!hasMadeSpawnSound)
    {
      soundManager.PlayAudio(soundManager.iceSwordSpawnClip, transform.position);
      hasMadeSpawnSound = true;
    }
    else
    {
      soundManager.PlayAudio(soundManager.iceSwordHitClip, transform.position);
    }
    //G�re la position
    FiringPos = transform.position;// AKA playerpos
    TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Met la icesword dans la bonne direction
    TargetPos = (TargetPos - FiringPos).normalized * Range + FiringPos; //Remet la icesword dans le range
  }

  // Update is called once per frame
  void Update()
  {
    CasterPosition = Player.transform.GetChild(0);
    DespawnTimer -= Time.deltaTime;
    SameCastOpportunity -= Time.deltaTime;

    if (DespawnTimer <= 0)
    {
      gameObject.SetActive(false);
    }
    else if (SameCastOpportunity <= 0 && slotInput.triggered)
    {
      TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Met la icesword dans la bonne direction
      TargetPos = (TargetPos - CasterPosition.position).normalized * Range + CasterPosition.position;//Remet la icesword dans le range


      SameCastOpportunity = CooldownBetweenSameCast;
    }

    transform.Translate((TargetPos - transform.position) * MovementSpeed);


    Rotation.SetRotation(CasterPosition.position);
  }
  public float GetCooldown()
  {
    return Cooldown;
  }

  public SpellBehaviour.SpellElements GetElements()
  {
    return Element;
  }

  public float GetKnockback()
  {
    //Besoin vue la rotation et collision g�rer dans l'autre code
    return KnockBackForce;
  }

  public float GetDamage()
  {
    return Damage + (Damage / 100) * Player.GetComponent<PlayerInventory>().GetPowerBonus();
  }

  public string GetDescription()
  {
    return Description;
  }
}
