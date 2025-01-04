using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IOnGameReset
{
  public static ScoreManager instance;
  public int enemisKilled { get; private set; } = 0;
  public int coinsCollected { get; private set; } = 0;
  public int nbItemsCollected { get; private set; } = 0;
  public int xp { get; private set; } = 0;
  List<GameObject> items = new List<GameObject>();

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

  private void Start()
  {
    DontDestroyManager.instance.AddItem(this);
  }


  public void IncrementXp()
  {
    xp++;

  }

  public void IncrementKills()
  {
    enemisKilled++;
    if (enemisKilled >= 100)
    {
      FindFirstObjectByType<AchievementManager>().SetAchievement("kill100");
    }
  }

  public void IncrementCoins()
  {
    coinsCollected++;
  }

  public void IncrementItems()
  {
    nbItemsCollected++;
  }

  public GameObject[] GetList()
  {
    return items.ToArray();
  }
  public void AddItem(GameObject item)
  {
    items.Add(item);
  }

  public void ResetGO()
  {
    nbItemsCollected = coinsCollected = enemisKilled = xp = 0;
    items.Clear();
  }
}
