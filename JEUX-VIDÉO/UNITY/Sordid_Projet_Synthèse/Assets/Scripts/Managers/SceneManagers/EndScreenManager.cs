using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        SceneManager.sceneUnloaded += HideCursor;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        LoadingScreen.instance.LoadLobby();
    }

    void HideCursor(Scene scene)
    {
        Cursor.visible = false;

    }

}
