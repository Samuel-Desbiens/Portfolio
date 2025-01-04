using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGhostBehaviour : MonoBehaviour
{
    const int HpStart = 5;
    int currentHealth;

    SoundManager smfx;

    bool allDead;

    [SerializeField] private SceneStateSave Save;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 5;

        smfx = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        if(Save.GhostState)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerDeathStart();
        }
        else if(collision.CompareTag("Weapon"))
        {
            smfx.PlayGhostHit();

            currentHealth--;
            
            if(currentHealth <= 0)
            {
                allDead = true;
                foreach( GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if(enemy.activeSelf && enemy != gameObject)
                    {
                        allDead = false;
                        break;
                    }
                }
                if(allDead)
                {
                    Save.GhostState = true;
                }
                gameObject.SetActive(false);
            }
        }
    }
}
           
