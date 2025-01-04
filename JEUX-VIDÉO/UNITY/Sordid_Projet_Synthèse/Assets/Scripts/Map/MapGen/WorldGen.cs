using Harmony;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    //asdasd
  [SerializeField] SpawnRoom startPrefab;
  [SerializeField] SpawnRoom endPrefab;
  [SerializeField] Room wallPrefab;

  [SerializeField] List<Room> roomPool;
    [SerializeField] Transform enemyParent;
    [SerializeField] Transform player;
    
  Dictionary<Node, Vector2Int> emptyNodes = new Dictionary<Node, Vector2Int>();

  SpawnRoom spawn;
  Room end;
   [SerializeField] Vector2 spawnPoint = Vector2.zero;

  [SerializeField] int roomsUntilEnd = 10;
  [SerializeField] int roomsTotal = 25;
  int roomsSpawned = 0;
  int tries = 0;
    [SerializeField] List<Enemy> enemyPool;
    [SerializeField] List<ChestBehavior> chestPool;
    [Header("Chest Spawn Rate as 1 in x, where x is inputed number")]
    [SerializeField] int chestSpawnRate = 10;


  Vector2Int currentPos = new Vector2Int (100, 100);
  //int gridSize = 5;
  bool[,] cells = new bool[400, 400];

  private void Start()
  {
        player = PlayerPersistence.instance.transform;
        player.transform.position = spawnPoint;
    Generate();
  }

  public void Generate()
  {
    SpawnStart();
    BuildMainPath();
    BuildOtherPaths();
    BlockRemainingNodes();
  }

  private void SpawnStart()
  {
    spawn = Instantiate(startPrefab, transform);
    spawn.transform.position = Vector2.zero;
    spawnPoint = spawn.getSpawnPos();
        spawn.SetSpawns(enemyPool, enemyParent, player);
        SetRoomCells(spawn.GetDimensions());
    AddEmptyNodes(spawn);
  }

  private void SpawnEnd(Room current)
  {
    Node last = null;
    Node inNode;
    do
    {
      List<Node> nodes = current.GetAvailable();
      if (nodes.Count != 0)
      {
        inNode = nodes.Random();
        currentPos += inNode.GetPos();
        Room newRoom;
        newRoom = SpawnNextRoom(inNode, current, endPrefab);
        if (newRoom == null)
        {
          Room wall = Instantiate(wallPrefab, transform);
          inNode.SetConnection(wall);
          wall.transform.position = inNode.transform.position + (Vector3.one / 12.5f);
        } else
        {
          end = newRoom;
        }
        currentPos -= inNode.GetPos();
      } else
      {
        nodes = current.GetUnavailable();
        //Get node that isnt towards last room
        do
        {
          if (nodes.Count == 1)
          {
            last = null;
          }
          inNode = nodes.Random();
        } while (inNode == last);
        //Get next room from node
        Room next = inNode.GetNext();
        if (next.GetGenre() != "Wall")
        {
          //Sets the last node to the node that connects to the current room
          foreach (Node node in next.GetUnavailable())
          {
            if (node.GetNext() == current)
            {
              last = node;
            }
          }
          currentPos += inNode.GetPos();
          MoveGridPosToNextRoom(last);
          current = next;
        }
      }
    } while (end == null);


  }

  private void BuildMainPath()
  {
    Room current = spawn;
    do
    {
      //Get the available nodes from the current room
      List<Node> nodes = current.GetAvailable();
      if (nodes.Count == 0) break;
      //Select one of the nodes
      Node inNode = nodes.Random();
      Vector2Int temp = currentPos;
      currentPos += inNode.GetPos();

      bool found = false;
      while (found == false)
      {
        //Get an other room
        Room next = roomPool.Random();
        //Check for nodes on the correct side of the room
        List<Node> nextNodes = next.GetCompatibleNodes(inNode.GetSide());
        if (nextNodes.Count == 0) continue;
        //Spawn the room according to the node positions
        Room newRoom = SpawnNextRoom(inNode, current, next);
        //If the room was spawned, set it as the current room and reset the tries
        //Otherwise increase the tries, try with another room and if it reaches 3, block the entrance.
        if (newRoom != null)
        {

          found = true;
          tries = 0;
          roomsSpawned++;
          current = newRoom;
        }
        else
        {
          tries++;
          if (tries > 3)
          {
            CreateWall(inNode);
            emptyNodes.Remove(inNode);
            currentPos = temp;
            tries = 0;
            break;
          }
        }
      }
    } while (roomsSpawned < roomsUntilEnd);
    SpawnEnd(current);
  }

  private void BuildOtherPaths()
  {
    while (roomsSpawned < roomsTotal)
    {
      RemoveCollidingDoorsFromEmptyNodes();
      //Get a random available node
      KeyValuePair<Node, Vector2Int> node = GetRandomEmptyNode();
      currentPos = node.Value;
      Node inNode = node.Key;
      if (inNode == null) break;
      bool found = false;
      do
      {
        //Get an other room
        Room next = roomPool.Random();
        //Check for nodes on the correct side of the room
        List<Node> nextNodes = next.GetCompatibleNodes(inNode.GetSide());
        if (nextNodes.Count == 0) continue;
        //Spawn the room according to the node positions
        Room newRoom = SpawnNextRoom(inNode, next);
        if (newRoom != null)
        {
          found = true;
          tries = 0;
          roomsSpawned++;
        } 
        else
        {
          tries++;
          if (tries >= 5)
          {
            CreateWall(inNode);
            emptyNodes.Remove(inNode);
            tries = 0;
            break;
          }
        }
      } while (found == false);
    }
  }

  private void RemoveCollidingDoorsFromEmptyNodes()
  {
    Dictionary<Node, Vector2Int> temp = emptyNodes;
    foreach (KeyValuePair<Node, Vector2Int> node1 in temp.ToListPooled())
    {
      foreach (KeyValuePair<Node, Vector2Int> node2 in temp.ToListPooled())
      {
        Vector2Int nodePos = node1.Value + new Vector2Int((int)node1.Key.GetSide(), 0);
        if (node2.Value == nodePos && node2.Key.GetSide() != node1.Key.GetSide())
        {
          RemoveUsedNode(node1.Key);
          RemoveUsedNode(node2.Key);
        }
      }
    }
  }
  private void BlockRemainingNodes()
  {
    Dictionary<Node, Vector2Int> temp = emptyNodes;
    foreach (KeyValuePair<Node, Vector2Int> inNode in temp.ToListPooled())
    {
      CreateWall(inNode.Key);
      RemoveUsedNode(inNode.Key);
    }
  }
  private Room SpawnNextRoom(Node inNode, Room inRoom, Room outRoomPrefab)
  {
    Room outRoom = Instantiate(outRoomPrefab, transform);
        Node outNode = outRoom.GetCompatibleNodes(inNode.GetSide()).Random();
    Vector2Int temp = currentPos;
    if (IsRoomConflicting(outRoom, outNode))
    {
      DestroyImmediate(outRoom.gameObject);
      currentPos = temp;
      return null;
    } else
    {
      //Connection
      inNode.SetConnection(outRoom);
      outNode.SetConnection(inRoom);
      //Set array cells
      SetRoomCells(outRoom.GetDimensions());
      //Get Nodes Left empty
      RemoveUsedNode(inNode);
      AddEmptyNodes(outRoom);
      RemoveUsedNode(outNode);
            //Position
            SetUpRoom(outRoom, GetNextRoomPosition(outRoom, outNode, inNode));

            return outRoom;
    }
  }

  //Doesn't set any connections
  private Room SpawnNextRoom(Node inNode, Room outRoomPrefab)
  {
    Room outRoom = Instantiate(outRoomPrefab, transform);
        Node outNode = outRoom.GetCompatibleNodes(inNode.GetSide()).Random();
    Vector2Int temp = currentPos;
    if (IsRoomConflicting(outRoom, outNode))
    {
      DestroyImmediate(outRoom.gameObject);
      currentPos = temp;
      return null;
    }
    else
    {
      //Set array cells
      SetRoomCells(outRoom.GetDimensions());
      //Get Nodes Left empty
      RemoveUsedNode(inNode);
      AddEmptyNodes(outRoom);
      RemoveUsedNode(outNode);
            //Position
            SetUpRoom(outRoom, GetNextRoomPosition(outRoom, outNode, inNode));
            return outRoom;
    }
  }
    void SetUpRoom(Room outRoom, Vector2 position)
    {
        outRoom.transform.position = position;
        outRoom.SetSpawns(enemyPool, enemyParent, player);
        int result = UnityEngine.Random.Range(0, chestSpawnRate);
        if(result == 0)
        {
            outRoom.SpawnChest(chestPool.Random());
        }
    }

    private bool IsRoomConflicting(Room room, Node entrance)
  {
    Vector2Int dimensions = room.GetDimensions();
    Vector2Int localPos = Vector2Int.zero;

    MoveGridPosToNextRoom(entrance);
    for (int i = 0; i < dimensions.x; i++)
    {
      localPos.x = i;
      for (int j = 0 ; j < dimensions.y; j++)
      {
        localPos.y = j;
        if(IsCellConflicting(currentPos+localPos)) return true;
      }
    }
    return false;
  }


  private bool IsCellConflicting(Vector2Int cell)
  {
    if (cell.x >= cells.Length || cell.x < 0 || cell.y >= cells.Length || cell.y < 0) return true;
    if (cells[cell.x, cell.y]) return true;
    return false;
  }

  private void SetRoomCells(Vector2Int dimensions)
  {
    for (int i = 0; i < dimensions.x; i++)
    {
      for (int j = 0; j < dimensions.y; j++)
      {
        cells[currentPos.x + i, currentPos.y + j] = true;
      }
    }
  }

  private Vector2 GetNextRoomPosition(Room outRoom, Node outNode, Node inNode)
  {
    Vector2 diff = outRoom.transform.position - outNode.transform.position;
    Vector2 ext = inNode.transform.position + (Vector3.right * 2 * (int)inNode.GetSide());
    return ext + diff;
  }
  private void MoveGridPosToNextRoom(Node entrance)
  {
    currentPos.x -= (int)entrance.GetSide();
    currentPos.x -= entrance.GetPos().x;
    currentPos.y -= entrance.GetPos().y;
  }

  private KeyValuePair<Node, Vector2Int> GetRandomEmptyNode()
  {
    int i = UnityEngine.Random.Range(1, emptyNodes.Count);
    foreach (KeyValuePair<Node, Vector2Int> pair in emptyNodes)
    {
      i--;
      if (i == 0)
      {
        return pair;
      }
    }
    return new KeyValuePair<Node, Vector2Int>();
  }

  private void RemoveUsedNode(Node nodeToDestroy)
  {
    if (emptyNodes.ContainsKey(nodeToDestroy))
    {
      emptyNodes.Remove(nodeToDestroy);
    }
  }

  private Vector3 GetRoomCenter(Room room)
  {
    Vector3 center = Vector3.zero;
    foreach (Node node in room.GetNodes())
    {
      center += node.transform.position;
    }
    center /= room.GetNodes().Count;
    return center;
  }

  private void CreateWall(Node inNode)
  {
    Room wall = Instantiate(wallPrefab, transform);
    inNode.SetConnection(wall);
    wall.transform.position = inNode.transform.position + Vector3.left + Vector3.up;
  }

  private void AddEmptyNodes(Room room)
  {
    List<Node> nodes = room.GetAvailable();
    foreach (Node node in nodes)
    {
      emptyNodes.Add(node, currentPos + node.GetPos());
    }
  }
}

