using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DontDestroyManager instance;
    List<IOnGameReset> dontDestroyList  = new List<IOnGameReset>();

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RemoveAllItems()
    {
        for (int i = 0; i < dontDestroyList.Count; i++)
        {
            dontDestroyList[i].ResetGO();
        }
    }

    public void AddItem(IOnGameReset go)
    {
        dontDestroyList.Add(go);
    }
}
