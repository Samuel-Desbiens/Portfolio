using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    Health health;
    Inventory inventory;
    void Start()
    {
        health = GetComponent<Health>();
        inventory = InventoryPersistence.instance.GetComponentInChildren<Inventory>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            inventory.AddCoin();
        }
        if (Input.GetKey(KeyCode.I))
        {
            health.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadingScreen.instance.LoadDeath();
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            LoadingScreen.instance.LoadScene(7);
        }
        if (Input.GetKey(KeyCode.K))
        {
            List<Health> enemies = FindObjectsByType<Health>(FindObjectsInactive.Exclude,FindObjectsSortMode.InstanceID).ToList();
            enemies.Remove(health);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].TakeDmg(10);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            LoadingScreen.instance.LoadScene(4);
        }
    }
}
