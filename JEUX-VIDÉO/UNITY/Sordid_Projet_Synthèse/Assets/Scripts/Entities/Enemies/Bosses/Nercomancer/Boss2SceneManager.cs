using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SceneManager : MonoBehaviour
{
    public void Win()
    {
        LoadingScreen.instance.LoadWin();
        AchievementManager.instance.SetAchievement("boss2");
    }
}
