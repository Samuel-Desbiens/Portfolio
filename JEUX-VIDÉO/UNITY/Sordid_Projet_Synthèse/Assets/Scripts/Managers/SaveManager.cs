using Harmony;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int coins = 0;
    public bool lockedSlot3 = true;
    public bool lockedSlot4 = true;
    public bool tutorialAchv = false;
    public bool lvl1Achv = false;
    public bool lvl2Achv = false;
    public bool boss1Achv = false;
    public bool boss2Achv = false;
    public bool kill100Achv = false;
    public bool buyShopAchv = false;
    public bool unlockPwrAchv = false;
}

public class SaveManager : MonoBehaviour
{

    private string filePath;
    private SaveData save = new();
    private Inventory inventory;


    #region Persistence
    public static SaveManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void OnApplicationQuit()
    {
        SetSave();
    }
    public void SetSave()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        save.coins = inventory.GetNbCoins();
        save.lockedSlot3 = inventory.GetHotbarSlot(2).IsLocked();
        save.lockedSlot4 = inventory.GetHotbarSlot(3).IsLocked();
        AchievementManager am = FindFirstObjectByType<AchievementManager>();
        save.tutorialAchv = am.GetAchievementDone("tutorial");
        save.lvl1Achv = am.GetAchievementDone("lvl1");
        save.lvl2Achv = am.GetAchievementDone("lvl2");
        save.boss1Achv = am.GetAchievementDone("boss1");
        save.boss2Achv = am.GetAchievementDone("boss2");
        save.kill100Achv = am.GetAchievementDone("kill100");
        save.buyShopAchv = am.GetAchievementDone("buyShop");
        save.unlockPwrAchv = am.GetAchievementDone("unlockPowers");
        SaveSaveToFile();
    }

    public SaveData GetSave()
    {
        return save;
    }

    private void SaveSaveToFile()
    {
        try
        {
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(filePath, json);
        }
        catch (IOException e)
        {
            Debug.LogError("Erreur lors de l'écriture dans le fichier : " + e.Message);
        }
    }

    public bool LoadSaveFromFile(String saveNb)
    {
        filePath = Application.persistentDataPath + "/save" + saveNb + ".json";
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                save = JsonUtility.FromJson<SaveData>(json);
                return true;
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur lors de la lecture du fichier : " + e.Message);
            }
        }
        return false;

    }
}