using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstance : MonoBehaviour
{
    static private CanvasInstance Instance;

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
