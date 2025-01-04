using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PermanentUpgradesManager : MonoBehaviour
{
    public static PermanentUpgradesManager instance;

    List<string> shops = new();
    List<string> soulsUnlocked = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddUnlockedSoul(GameObject soul)
    {
        soulsUnlocked.Add(soul.name);
    }

    public void LoadAllUnlockedSouls()
    {
        List<Souls> allSouls = GameObject.FindObjectsByType<Souls>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        string[] soulNames = new string[allSouls.Count];
        for (int i = 0; i < soulNames.Length; i++)
        {
            soulNames[i] = allSouls[i].gameObject.name;
        }
        for (int i = 0; i < soulNames.Length; i++)
        {
            if (soulsUnlocked.Contains(soulNames[i]))
            {
                allSouls[i].gameObject.SetActive(true);
            }
        }
    }

    public void UnloadAllUsedShops()
    {
        for (int i = 0; i < shops.Count; i++)
        {
            GameObject.Find(shops[i]).SetActive(false);
        }
    }
    public void AddUsedShop(PermenantUpgradeShop shop)
    {
        shops.Add(shop.gameObject.name);
    }
}
