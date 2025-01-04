using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void OnQuit()
    {
        Application.Quit();
    }

    public void onClickStartGame(int saveId)
    {
        //GameManager.Instance.StartNextlevel(2.0f);

        SaveManager saveManager = FindFirstObjectByType<SaveManager>();
        bool playTutorial = saveManager.LoadSaveFromFile(saveId.ToString());
        Debug.Log(saveManager.GetSave());
        FindFirstObjectByType<AchievementManager>().LoadAchievementsFromSave(saveManager.GetSave());

        if (!playTutorial)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2, LoadSceneMode.Single);
        }
    }
}
