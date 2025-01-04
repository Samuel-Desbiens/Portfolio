using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementMenuManager : MonoBehaviour
{
    AchievementBox[] allBoxes;


    private void Awake()
    {
        allBoxes = GetComponentsInChildren<AchievementBox>(true);
    }

    public void SetAllAchievements(Achievement[] achievements)
    {
        for (int i = 0; i < allBoxes.Length; i++)
        {
            allBoxes[i].SetAchievement(achievements[i]);
        }
    }
}
