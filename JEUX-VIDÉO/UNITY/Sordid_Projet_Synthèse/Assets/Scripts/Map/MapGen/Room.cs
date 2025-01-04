using Harmony;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] string genre;
  [SerializeField] Vector2Int dimension;
  [SerializeField] List<Node> nodes;
    [SerializeField] Transform spawnPointParent;
    [SerializeField] int maxRoomPowerPoints;
    private List<Enemy> possibleEnnemis;
    List<GameObject> activeSpawnPoints => spawnPointParent.Children().ToList();
    int powerPointsExpanded;




    #region Created

    public Vector2Int GetDimensions() { return dimension; }

  public string GetGenre()
  {
    return genre;
  }
  public List<Node> GetCompatibleNodes(Side side)
  {
    List<Node> compatibles = new List<Node>();
    foreach (Node node in nodes)
    {
      if (node.GetSide() != side)
      {
        compatibles.Add(node);
      }
    }
    return compatibles;
  }

  public List<Node> GetUnavailable()
  {
    List<Node> unavailable = new List<Node>();
    foreach (Node node in nodes)
    {
      if (node.IsUsed())
      {
        unavailable.Add(node);
      }
    }
    return unavailable;
  }

  public List<Node> GetNodes()
  {
    return nodes;
  }
  #endregion


  #region Creating
  public List<Node> GetAvailable()
  {
    List<Node> available = new List<Node>();
    foreach (Node node in nodes)
    {
      if (!node.IsUsed())
      {
        available.Add(node);
      }
    }
    return available;
  }
    #endregion

    #region  Spawning

    public void SpawnChest(ChestBehavior chest)
    {
        if(activeSpawnPoints.Count > 0)
        {
            chest = Instantiate(chest, transform, true); ;
            chest.gameObject.SetActive(true);
            chest.transform.position = activeSpawnPoints.Random().transform.position;
        }
    }

    public void SetSpawns(List<Enemy> enemyList, Transform parent, Transform player)
    {
        possibleEnnemis = enemyList;
        while (powerPointsExpanded < maxRoomPowerPoints && activeSpawnPoints.Count > 0)
        {
            Spawn(parent, player);
        }
    }

    void Spawn(Transform parent, Transform player )
    {

        GameObject selectedSpawn = activeSpawnPoints.Random();
        activeSpawnPoints.Remove(selectedSpawn);
        Vector2 spawnPoint = selectedSpawn.transform.position;

        Enemy enemy = possibleEnnemis.Random();
        Enemy instance = Instantiate(enemy, parent).GetComponent<Enemy>();
        instance.SetPlayer(player);
        instance.Spawn(spawnPoint);
        powerPointsExpanded += instance.GetPowerPoints();
        
    }
    #endregion
}
