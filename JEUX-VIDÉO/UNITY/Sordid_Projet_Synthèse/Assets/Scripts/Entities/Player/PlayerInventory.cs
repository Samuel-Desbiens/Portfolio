using Harmony;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInventory : MonoBehaviour
{

    private PlayerInputActions inputs;
    private InputAction inventoryInput;
    private InputAction potionInput;
    private bool coinBoostIsActive = false;
    private bool powerBoostIsActive = false;
    Inventory inventory;
    float powerBonusAmount = 0;
    SoundManager soundManager;
    [SerializeField] Transform spellCollected;
    [SerializeField] float potionHeal;
    private void Awake()
    {
        inputs = new PlayerInputActions();
        inventoryInput = inputs.Player.Inventory;
        potionInput = inputs.Player.Potion;
    }
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        soundManager = SoundManager.Instance;
    }
    private void OnEnable()
    {
        inventoryInput.Enable();
        potionInput.Enable();
    }

    private void OnDisable()
    {
        inventoryInput.Disable();
        potionInput.Disable();
    }

    void Update()
    {
        ManageInventory();
        powerBonusAmount = inventory.GetPowerBonus();
    }

    public void SetIncreaseCoinPickup(bool value)
    {
        coinBoostIsActive = value;
    }

    public void SetIncreasePowerPickup(bool value)
    {
        powerBoostIsActive = value;
    }

    public void TryAddItem(GameObject obj)
    {
        if (inventory == null)
        {
            return;
        }

        if (obj.CompareTag("Boot") || obj.CompareTag("Cape") || obj.CompareTag("SpellScroll"))
        {
            if (inventory.AddToInventory(obj))
            {
                obj.SetActive(false);
                obj.transform.parent = spellCollected;
            }
        }
        else if (obj.CompareTag("PowerBoost"))
        {
            if (powerBoostIsActive && Random.Range(1, 4) % 2 == 0)
            {
                inventory.AddPowerBonus();
            }
            inventory.AddPowerBonus();
        }
        else if (obj.CompareTag("HealthPotion"))
        {
            inventory.AddHealthPotion();
        }
        else if (obj.CompareTag("Coin"))
        {
            if (coinBoostIsActive && Random.Range(1, 3) % 2 == 0)
            {
                inventory.AddCoin();
            }
            inventory.AddCoin();
            soundManager.PlayAudio(soundManager.coinPickupClip, transform.position);
        }


    }

    public void SetIncreasePower(int value)
    {
        inventory.SoulIncreasePower(value);
    }
    public void SetDecreasePower(int value)
    {
        inventory.SoulDecreasePower(value);
    }

    void ManageInventory()
    {
        if (inventoryInput.triggered)
        {
            if (inventory.isActiveAndEnabled)
            {
                inventory.gameObject.SetActive(false);
            }
            else
            {
                inventory.gameObject.SetActive(true);
            }
        }
        if (potionInput.triggered && inventory.HasHealthPotions())
        {
            gameObject.GetComponent<Health>().Heal(potionHeal);
            inventory.RemoveHealthPotion();
        }
    }
    public float GetPowerBonus()
    {
        return powerBonusAmount;
    }
}
