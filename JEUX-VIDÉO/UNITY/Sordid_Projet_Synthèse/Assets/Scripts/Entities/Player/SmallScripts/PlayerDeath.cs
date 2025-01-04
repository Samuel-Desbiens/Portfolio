using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public void OnDeath()
    {
        LoadingScreen.instance.LoadDeath();

    }
}
