using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : Room
{
  [SerializeField] Vector2 spawnPos;

  public Vector2 getSpawnPos()
  {
    return spawnPos;
  }
}
