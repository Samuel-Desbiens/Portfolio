using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour, IItemInfo
{
    public BootsMold mold;
    string bootsName;
    float speed;
    string description;
    BootsAbility ability;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = mold.bootSprite;
        bootsName = mold.name;
        speed = mold.moveSpeedBoost;
        description = mold.description;
        ability = mold.ability;
    }
    
    public BootsAbility GetAbility()
    {
        return ability;
    }

    public float GetSpeed()
    {
        return speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().TryAddItem(this.gameObject);
        }
    }

    public string GetItemInfo()
    {
        return mold.description;
    }
}
