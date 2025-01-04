using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersBehaviour : MonoBehaviour
{
    const int HitPointStart = 250;
    int HitPoints;

    GameManager gameManager;

    bool Team; //True = Green && False = Blue

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        HitPoints = HitPointStart;

        if(gameObject.name.Contains("G"))
        {
            Team = true;
        }
        else
        {
            Team = false;
        }

        gameManager.AddTower(gameObject);
    }

    public void TakeDMG(int dmg)
    {
        HitPoints += dmg;
        if (HitPoints <= 0)
        {
            gameManager.RemoveTower(gameObject);
            gameObject.SetActive(false);
        }
    }

    public bool GetTeam()
    {
        return Team;
    }
}
