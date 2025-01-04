using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class LeafShieldBehaviour : MonoBehaviour, SpellBehaviour
{
  private float damage = 5;
  private float cooldown = 8;
  private float movementSpeed = 10;
  private float rotationSpeed = 120;
  private float knockback = 0;

  private SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Nature;

    private const string Description = "A spinning shield made of leaf. (I swear it protects you)";

  SoundManager soundManager;

  private const float SpawnDelayEach = 0.25f;
  private const float WaitBeforeStartDespawn = 2.25f;
  private float Timer;

  private Vector3 Target;
  private bool Thrown;

  private GameObject Player;

  private PlayerInputActions pia;
  private InputAction slotInput;


  private PlayerInventory playerInventory;
  private void Awake()
  {
    cooldown += (transform.childCount / 3);
    Player = GameObject.Find("Player");
    soundManager = SoundManager.Instance;
  }


  private void OnEnable()
  {
    playerInventory = Player.GetComponent<PlayerInventory>();
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
    Thrown = false;

    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(false);
    }
  }

  private void Cast()
  {
    StartCoroutine(SpawnAndDespawnLeaf());
  }

  private void Update()
  {
    transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

    Timer += Time.deltaTime;

    if (!Thrown)
    {
      transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

      transform.position = Player.transform.position;
      if (slotInput.triggered)
      {
        Thrown = true;

        Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      }
    }
    else
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.x, Target.y, transform.position.z), movementSpeed * Time.deltaTime);
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
    return Element;
  }
  public float GetKnockback()
  {
    return knockback;
  }

  public bool GetThrown()
  {
    return Thrown;
  }
  public string GetDescription()
  {
    return Description;
  }

  //Coroutine
  IEnumerator SpawnAndDespawnLeaf()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(true);
      soundManager.PlayAudio(soundManager.leaf1Clip, transform.position);
      yield return new WaitForSeconds(SpawnDelayEach);
    }
    yield return new WaitForSeconds(WaitBeforeStartDespawn);
    for (int o = 0; o < transform.childCount; o++)
    {
      transform.GetChild(o).gameObject.SetActive(false);

      yield return new WaitForSeconds(SpawnDelayEach);
    }
    gameObject.SetActive(false);
  }
}
