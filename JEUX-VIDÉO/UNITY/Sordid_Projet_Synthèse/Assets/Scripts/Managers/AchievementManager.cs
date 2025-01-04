using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{

    #region Persistence
    public static AchievementManager instance;

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

    IDictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    [SerializeField] Sprite[] sprites;
    public void SetAchievement(string name)
    {
        achievements[name].SetDone(true);
    }

    public bool GetAchievementDone(string name)
    {
        return achievements[name].IsDone();
    }

    public Achievement[] GetAchievements()
    {
        return achievements.Values.ToArray();
    }
    public void LoadAchievementsFromSave(SaveData save)
    {
        achievements["tutorial"].SetDone(save.tutorialAchv);
        achievements["lvl1"].SetDone(save.lvl1Achv);
        achievements["lvl2"].SetDone(save.lvl2Achv);
        achievements["boss1"].SetDone(save.boss1Achv);
        achievements["boss2"].SetDone(save.boss2Achv);
        achievements["kill100"].SetDone(save.kill100Achv);
        achievements["buyShop"].SetDone(save.buyShopAchv);
        achievements["unlockPowers"].SetDone(save.unlockPwrAchv);
    }

    void Start()
    {
        achievements.Add(new("tutorial", new("OverAchiever", "Complete the tutorial.", sprites[0])));
        achievements.Add(new("lvl1", new("Dungeon Expert", "Complete the first level.", sprites[1])));
        achievements.Add(new("lvl2", new("Dungeon Legend", "Complete the second level", sprites[2])));
        achievements.Add(new("boss1", new("Rawr", "Defeat the first boss and gain a new soul.", sprites[3])));
        achievements.Add(new("boss2", new("See, him face", "Defeat the second boss and gain a new soul.", sprites[4])));
        achievements.Add(new("kill100", new("Killing Arcana", "Defeat 100 enemies in a single run.", sprites[5])));
        achievements.Add(new("buyShop", new("Supreme", "Purchase something from the shop.", sprites[6])));
        achievements.Add(new("unlockPowers", new("Unstoppable", "Unlock all of your locked spellslots.", sprites[7])));
    }


}
