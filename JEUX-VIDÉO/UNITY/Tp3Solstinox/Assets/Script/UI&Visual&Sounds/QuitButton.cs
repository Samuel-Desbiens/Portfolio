using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
   public void OnQuitPress()
    {
        Application.Quit();
        //Si L'application a pas quitté alors nous somme dans l'éditeur
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
