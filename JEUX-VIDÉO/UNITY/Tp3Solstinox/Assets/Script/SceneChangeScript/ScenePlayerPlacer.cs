using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlayerPlacer : MonoBehaviour
{
    GameObject Player;

    Direction ToPlace;

    string CheckWord;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        if(Player != null)
        {
            Player.SetActive(false);

            ToPlace = Player.GetComponent<PlayerController>().GetSceneDirection();

            if (ToPlace == Direction.Up)
            {
                CheckWord = "Down";
            }
            else if (ToPlace == Direction.Down)
            {
                CheckWord = "Up";
            }
            else if (ToPlace == Direction.Right)
            {
                CheckWord = "Left";
            }
            else
            {
                CheckWord = "Right";
            }

            foreach (Transform child in transform)
            {
                if (child.name.Contains(CheckWord))
                {
                    Player.GetComponent<PlayerController>().TeleportPlayer(child.position);
                    Player.SetActive(true);
                    break;
                }
            }
        }  
    }
}
