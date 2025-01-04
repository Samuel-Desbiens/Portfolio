using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Green,
    Blue,
    Pink,
    White
}

public enum Direction
{
    Up = 0,
    Right,
    Down,
    Left
}



public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    private const int NumberOfWeapon = 10;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            GameObject empty = new GameObject("Projectile");

            empty.transform.position = new Vector3(0, 100, 0);

            DontDestroyOnLoad(empty);

            for (int i = 0; i < NumberOfWeapon; i++)
            {
                GameObject tempPro = (GameObject)Instantiate(Resources.Load("Prefabs/Shuriken"));

                tempPro.SetActive(false);

                tempPro.name = "Weapon" + (i + 1).ToString();

                tempPro.transform.SetParent(empty.transform);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
}
