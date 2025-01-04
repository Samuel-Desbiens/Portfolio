using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour, IOnGameReset
{
    [SerializeField] Hotbarmanager hotbarmanager;
    [SerializeField] HotbarSlot[] hotbarSlots;
    [SerializeField] InventorySlot[] invSlots;
    [SerializeField] GameObject baseSoul;
    [SerializeField] float capeMaxHealthBonus = 50;
    UltimateSlot ultimateSlot;
    AbilitySlot abilitySlot;
    GameObject suspendedItem;
    BootSlot bootSlot;
    CapeSlot capeSlot;
    DisplayPotion potions;
    DisplayCoins coins;
    Slot suspendedItemSlot;
    bool capeBonusApplied = false;
    PlayerMovement player;
    float[] playerStats = { 0, 0, 0, 0, 0, 0 };
    float currentSpeedBoost = 0;
    [SerializeField] int powerBonus = 0;
    int healthPotions = 0;
    [SerializeField] int nbCoins = 0;
    PlayerInputActions pia;
    InputAction deleteInput;

    const int INVENTORY_FULL_FLAG = -1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyManager.instance.AddItem(this);
        player = FindAnyObjectByType<PlayerMovement>();
        capeSlot = FindAnyObjectByType<CapeSlot>();
        abilitySlot = FindAnyObjectByType<AbilitySlot>();
        bootSlot = FindAnyObjectByType<BootSlot>();
        ultimateSlot = FindAnyObjectByType<UltimateSlot>();
        potions = FindAnyObjectByType<DisplayPotion>();
        coins = FindAnyObjectByType<DisplayCoins>();
        LoadSave();
        //SetSoul(baseSoul);
    }

    private void Awake()
    {
        pia = new PlayerInputActions();
        deleteInput = pia.Player.InventoryDelete;
    }

    private void OnEnable()
    {
        deleteInput.Enable();
    }

    private void OnDisable()
    {
        deleteInput.Disable();
    }
    void Update()
    {
        if (gameObject.activeSelf && suspendedItem != null)
        {
            if (deleteInput.triggered)
            {
                suspendedItemSlot.RemoveItem();
                suspendedItem = null;
                suspendedItemSlot = null;
            }
        }
    }

    public UltimateSlot GetUltimateSlot()
    {
        return ultimateSlot;
    }

    public void SetSoul(GameObject soul)
    {
        Souls newSoul = soul.GetComponent<Souls>();
        if (ultimateSlot.GetItem() != null)
        {
            Souls currentSoul = ultimateSlot.GetItem().GetComponent<Souls>();
            currentSoul.RemoveActiveAbility();
            currentSoul.RemovePassiveAbility();
            ultimateSlot.RemoveItem();
            Destroy(currentSoul);
        }
        Souls soulInstance = Instantiate(newSoul.gameObject, player.transform).GetComponent<Souls>();
        ultimateSlot.SetSoul(soulInstance.gameObject);
        newSoul.ApplyPassiveAbility();
        soulInstance.transform.parent = player.transform;
        soulInstance.gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }

    void LoadSave()
    {
        SaveData save = SaveManager.instance.GetSave();
        nbCoins = save.coins;
        coins.UpdateNbCoins(nbCoins);
        if (!save.lockedSlot3) hotbarSlots[2].UnlockSlot();
        if (!save.lockedSlot4) hotbarSlots[3].UnlockSlot();
    }

    int FindAvailableSlot(Slot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())
            {
                return i;
            }
        }
        return INVENTORY_FULL_FLAG;
    }


    public void UnlockSlot()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i].UnlockSlot())
            {
                return;
            }
            FindFirstObjectByType<AchievementManager>().SetAchievement("unlockPowers");

        }
    }

    public void AddToInventoryEvent(GameObject item)
    {
        AddToInventory(item);
    }
    public bool AddToInventory(GameObject item)
    {
        ScoreManager score = ScoreManager.instance;
        if (item.CompareTag("Boot"))
        {
            if (bootSlot.IsEmpty())
            {
                bootSlot.AddItem(item);
                abilitySlot.UpdateBootSkill(item);
                ApplyStatChanges();
                score.IncrementItems();
                score.AddItem(item);
                return true;
            }
        }
        else if (item.CompareTag("Cape"))
        {
            if (capeSlot.IsEmpty())
            {
                capeSlot.AddItem(item);
                ApplyStatChanges();
                score.IncrementItems();
                score.AddItem(item);
                return true;
            }
        }
        else if (item.CompareTag("SpellScroll"))
        {
            int hotbarSlotToAddItem = FindAvailableSlot(hotbarSlots);
            if (hotbarSlotToAddItem != INVENTORY_FULL_FLAG)
            {
                hotbarSlots[hotbarSlotToAddItem].AddItem(item);
                score.IncrementItems();
                score.AddItem(item);
                return true;
            }
        }

        int invSlotToAddItem = FindAvailableSlot(invSlots);
        if (invSlotToAddItem != INVENTORY_FULL_FLAG)
        {
            score.IncrementItems();
            score.AddItem(item);
            invSlots[invSlotToAddItem].AddItem(item);
            return true;
        }
        return false;
    }

    public GameObject GetSuspendedItem()
    {
        return suspendedItem;
    }

    public void SuspendSlotItem(Slot slot)
    {
        suspendedItem = slot.GetItem();
        suspendedItemSlot = slot;
        if (suspendedItem != null)
        {
            slot.SetSelectedmaterial();
        }
    }

    public void RemoveSuspendedItem()
    {
        suspendedItemSlot.RemoveItem();
        suspendedItemSlot = null;
        suspendedItem = null;
    }

    void SwitchItems(Slot slot)
    {
        suspendedItemSlot.RemoveItem();
        suspendedItemSlot.AddItem(slot.GetItem());
        slot.RemoveItem();
        slot.AddItem(suspendedItem);
        suspendedItemSlot.SetUnselectedMaterial();
        slot.SetUnselectedMaterial();
        suspendedItem = null;
        suspendedItemSlot = null;
        ApplyStatChanges();
    }

    public void CheckToSwitchItems(Slot slot)
    {
        if (slot != suspendedItemSlot)
        {
            if (suspendedItemSlot.IsSlotCompatible(slot))
            {
                if (slot.GetItem() == null ||
                    (slot.GetItem().tag != "SpellScroll" &&
                    suspendedItemSlot.GetItem().tag != "SpellScroll") ||
                    !GameObject.ReferenceEquals(slot.GetItem().GetComponent<SpellScrolls>().GetSpell(), suspendedItemSlot.GetItem().GetComponent<SpellScrolls>().GetSpell()) ||
                    slot.GetItem().GetComponent<SpellScrolls>().GetUpgraded()) //La Ligne de l'enfer ...
                {
                    SwitchItems(slot);
                }
                else
                {
                    RemoveSuspendedItem();
                    slot.GetItem().GetComponent<SpellScrolls>().Upgrade();
                    slot.UpdateItemSprite();
                    suspendedItemSlot = null;
                }
            }
        }
    }

    public void AddPowerBonus()
    {
        powerBonus++;
        ScoreManager.instance.IncrementXp();

    }

    public void AddCoin()
    {
        nbCoins++;
        coins.UpdateNbCoins(nbCoins);
        ScoreManager.instance.IncrementCoins();

    }

    public void RemoveCoins(int nbCoinsRemoved)
    {
        nbCoins -= nbCoinsRemoved;
        coins.UpdateNbCoins(nbCoins);
    }

    public int GetNbCoins()
    {
        return nbCoins;
    }

    public void SoulIncreasePower(int powerIncreaseAmount)
    {
        powerBonus += powerIncreaseAmount;
    }

    public void SoulDecreasePower(int powerIncreaseAmount)
    {
        powerBonus -= powerIncreaseAmount;
    }

    public void AddHealthPotion()
    {
        healthPotions++;
        potions.UpdateNbPotion(healthPotions);
    }

    public void RemoveHealthPotion()
    {
        if (healthPotions > 0)
        {
            healthPotions--;
            potions.UpdateNbPotion(healthPotions);
        }
    }

    public bool HasHealthPotions()
    {
        return healthPotions > 0;
    }

    public GameObject GetSpellInSlot(int slot) //slot 0 = base spell && slot 4 = ultimate
    {
        if (hotbarSlots[slot].GetItem() != null)
        {
            return hotbarSlots[slot].GetItem().GetComponent<SpellScrolls>().GetSpell();
        }
        else
        {
            return null;
        }
    }

    public HotbarSlot GetHotbarSlot(int slot)
    {
        return hotbarSlots[slot];
    }

    public int GetHotbarSlotsLength()
    {
        return hotbarSlots.Length;
    }

    public BootsAbility GetCurrentBootAbility()
    {
        return bootSlot.GetAbility();
    }

    public float[] GetPlayerStats()
    {
        return playerStats;
    }

    public float GetSpeedBoost()
    {
        return currentSpeedBoost;
    }

    public int GetPowerBonus()
    {
        return powerBonus;
    }

    public int GetHealthPotions()
    {
        return healthPotions;
    }

    private void ApplyStatChanges()
    {
        if (capeSlot.GetItem() != null && !capeBonusApplied)
        {
            player.IncreaseMaxHealth(capeMaxHealthBonus);
            capeBonusApplied = true;
        }
        else if(capeBonusApplied)
        {
            player.DecreaseMaxHealth(capeMaxHealthBonus);
            capeBonusApplied = false;
        }

        currentSpeedBoost = bootSlot.GetBootsSpeedValue();
        player.SetSpeedBoost();
    }

    public void ResetGO()
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            invSlots[i].RemoveItem();
        }
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].RemoveItem();
        }
        healthPotions = 0;
        potions.UpdateNbPotion(healthPotions);
        powerBonus = 0;
        bootSlot.RemoveItem();
        capeSlot.RemoveItem();
    }
}
