using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] GameObject archer;
  [SerializeField] GameObject spearman;

  private static int enemyAmount = 5;
  private GameObject[] enemies = new GameObject[enemyAmount];
  [SerializeField] private float spawnCooldown = 5f;
  private float spawnTimer = 0f;
  int spawnedEnemies = 0;

  void Start()
  {
    for (int i = 0; i < enemyAmount; i++)
    {
      GameObject toSpawn;
      int rnd = Random.Range(1, 3);
      if (rnd == 1)
      {
        toSpawn = archer;
      } else
      {
        toSpawn = spearman;
      }
      PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
      toSpawn.GetComponent<GroundedEnemyBehavior>().SetPlayer(player.transform);
      enemies[i] = Instantiate(toSpawn);
      enemies[i].SetActive(false);
    }
  }

  void Update()
  {
    spawnTimer += Time.deltaTime;
    if (spawnTimer >= spawnCooldown)
    {
      spawnTimer = 0f;
      SpawnEnemy();
    }
  }

  public void SpawnEnemy()
  {
    if (spawnedEnemies < 5)
    {
      for (int i = 0; i < enemyAmount; i++)
      {
        if (!enemies[i].activeSelf)
        {
          enemies[i].transform.position = transform.position;
          enemies[i].SetActive(true);
          spawnedEnemies++;
          return;
        }
      }
    }
  }

}
