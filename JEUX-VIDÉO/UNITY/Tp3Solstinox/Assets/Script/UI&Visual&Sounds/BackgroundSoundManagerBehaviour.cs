using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManagerBehaviour : MonoBehaviour
{
    static private BackgroundSoundManagerBehaviour Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
