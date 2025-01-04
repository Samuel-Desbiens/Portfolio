using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Harmony;

public class HotbarSlot : Slot
{
  [SerializeField] Sprite unlockedSprite;
  Image lockedImage;
  protected Timer cooldownTimer;
  float cooldown = 1;

  DisplayCooldown cooldownDisplay;
  [SerializeField] string inputAction;
  InputAction spellInput;
  override protected void Start()
  {
    lockedImage = GetComponent<Image>();
    slotImage = transform.GetChild(0).GetComponent<Image>();
    cooldownTimer = new(cooldown);
    cooldownDisplay = transform.Find("Cooldown").GetComponent<DisplayCooldown>();
    SetCooldown(cooldown);
    base.Start();
  }
  // Update is called once per frame

  public bool UnlockSlot()
  {
    if (isLocked)
    {
      isLocked = false;
      lockedImage.sprite = unlockedSprite;
      return true;
    }
    return false;
  }



  private void Awake()
  {
    spellInput = new PlayerInputActions().FindAction(inputAction);
  }

  private void OnEnable()
  {
    spellInput.Enable();
  }

  private void OnDisable()
  {
    spellInput.Disable();
  }

  void Update()
  {
    cooldownTimer.Update();
    cooldownDisplay.UpdateCooldownTime(cooldownTimer.GetTimeLeft());
  }

  public InputAction GetInput()
  {
    return spellInput;
  }

  public string GetInputName()
  {
    return spellInput.name;
  }
  public bool OffCooldown()
  {
    return cooldownTimer.CanDo();
  }

  public void SetCooldown(float newCooldown)
  {
    cooldownDisplay.SetMaxCooldownTime(newCooldown);
    cooldownTimer.SetCooldown(newCooldown);
    cooldownTimer.Reset();
  }
}
