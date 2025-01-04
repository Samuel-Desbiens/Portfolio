using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private SceneStateSave Save;

    private void Awake()
    {
        if(Save.TokenState)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInventory>().GainBall();

            Save.TokenState = true;

            gameObject.SetActive(false);
        }


    }
}
