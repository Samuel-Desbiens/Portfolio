using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class FireBallBehaviour : MonoBehaviour, SpellBehaviour
{
  private float damage = 30f;
  private float cooldown = 6f;

  private float knockback = 100f;

  private Vector3 firingPos;
  private float angle;

  SoundManager soundManager;

  private const float movementSpeed = 10f;

  private const SpellBehaviour.SpellElements element = SpellBehaviour.SpellElements.Fire;

  private const string Description = "The cause for the most accidentals deaths for wizards!";

  private FireBallRotationAndCollision rotation;

    private GameObject fireBallForm;
    private GameObject explosionForm;

    private PlayerInventory playerInventory;

  private void Awake()
  {
    rotation = transform.GetChild(0).GetComponent<FireBallRotationAndCollision>();
    fireBallForm = transform.GetChild(0).gameObject;
    explosionForm = transform.GetChild(1).gameObject;
    soundManager = SoundManager.Instance;
  }

  private void OnEnable()
  {
    playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    ResetSpell();
    Cast();
  }

  private void ResetSpell()
  {
    if (!transform.GetChild(0).gameObject.activeSelf)
    {
      ResetState();
    }
  }

  private void Cast()
  {
    firingPos = transform.position;

    rotation.SetRotation(firingPos);
    soundManager.PlayAudio(soundManager.fireBallProjClip, transform.position);

        Vector3 MouseToCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        angle = Mathf.Atan2((MouseToCamPos - firingPos).y, (MouseToCamPos - firingPos).x);
    }

  // Update is called once per frame
  void Update()
  {
    if (transform.GetChild(0).gameObject.activeSelf)
    {
      Vector2 NextPoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

      transform.Translate(NextPoint * movementSpeed * Time.deltaTime);
    }
  }

  public float GetCooldown()
  {
    return cooldown;
  }

  public float GetDamage()
  {
    return damage + (damage / 100) * playerInventory.GetPowerBonus();
  }

  public SpellBehaviour.SpellElements GetElements()
  {
    return element;
  }
  public float GetKnockback()
  {
    return knockback;
  }

  public void Explode()
  {
    fireBallForm.SetActive(false);
    explosionForm.SetActive(true);
  }
  public void ResetState()
  {
    fireBallForm.SetActive(true);
    explosionForm.SetActive(false);
  }

  public string GetDescription()
  {
    return Description;
  }
}
