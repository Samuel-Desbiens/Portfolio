using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBehavior : MonoBehaviour
{
  private PlayerInputActions pia;
  private InputAction interact;
  protected bool playerIsInTrigger = false;
  protected bool isOpen = false;
  protected SpriteRenderer spriteRenderer;

  private void Awake()
  {
    pia = new PlayerInputActions();
    interact = pia.Player.Interact;
  }

  private void OnEnable()
  {
    interact.Enable();
  }

  private void OnDisable()
  {
    interact.Disable();
  }


  // Update is called once per framef
  void Update()
  {
    if (playerIsInTrigger && interact.triggered && !isOpen)
    {
      OnOpen();
    }
  }

  protected virtual void OnOpen()
  {
    isOpen = true;
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      playerIsInTrigger = true;

    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    playerIsInTrigger = false;
  }

}
