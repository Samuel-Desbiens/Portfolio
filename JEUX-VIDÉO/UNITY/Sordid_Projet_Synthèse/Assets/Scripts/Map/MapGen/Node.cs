using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
  Room connection;
  [SerializeField] Side side;
  [SerializeField] Vector2Int localPos;

  public Vector2Int GetPos()
  {
    return localPos;
  }
  public Side GetSide()
  {
    return side;
  }

  public bool IsUsed()
  {
    if (connection == null)
    {
      return false;
    }
    return true;
  }

  public void SetConnection(Room room)
  {
    connection = room;
  }

  public Room GetNext()
  {
    return connection;
  }

}
