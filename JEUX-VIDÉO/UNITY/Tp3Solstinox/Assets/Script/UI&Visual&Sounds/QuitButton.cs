using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
   public void OnQuitPress()
    {
        Application.Quit();
        //Si L'application a pas quitt� alors nous somme dans l'�diteur
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
