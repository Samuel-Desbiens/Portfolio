using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
  public static PlayerPersistence instance;

  private void Awake()
  {
    if (instance == null)
    {

      instance = this;
      DontDestroyOnLoad(gameObject);
      //DontDestroyManager.instance.AddItem(this.gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }


}
