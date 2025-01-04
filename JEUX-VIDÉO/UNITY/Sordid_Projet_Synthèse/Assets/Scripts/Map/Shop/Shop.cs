using Harmony;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
  Inventory inventory;
  [SerializeField] TextMeshPro text;
  [SerializeField] ItemBehavior item;
  PlayerInputActions pia;
  InputAction interact;
  // Start is called before the first frame update
  void Start()
  {
    inventory = InventoryPersistence.instance.GetComponentInChildren<Inventory>();
    //TODO : put actual gold value here
    text.text = item.GetPrice().ToString();
  }

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
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      //TODO replace with input system
      if (interact.triggered)
      {
        TryBuy();
      }
    }
  }

  protected virtual void Buy()
  {
    FindFirstObjectByType<AchievementManager>().SetAchievement("buyShop");
    item.Buy();
    inventory.RemoveCoins(item.GetPrice());
    text.text = "Sold out !";
  }

  void TryBuy()
  {

    int money = inventory.GetNbCoins();
    if (item.gameObject.activeSelf && money >= item.GetPrice())
    {

      Buy();
    }

  }
}
