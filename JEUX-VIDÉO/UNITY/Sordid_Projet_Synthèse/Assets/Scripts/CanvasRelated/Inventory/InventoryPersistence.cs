using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPersistence : MonoBehaviour
{
  public static InventoryPersistence instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(instance.gameObject);

    }
    else
    {
      Destroy(gameObject);
    }
  }
}
