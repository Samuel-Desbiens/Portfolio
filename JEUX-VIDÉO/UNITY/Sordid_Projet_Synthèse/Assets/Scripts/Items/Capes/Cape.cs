using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cape : MonoBehaviour, IItemInfo
{
    public CapeMold mold;
    string capename;
    string description;
    float healthBoost;
    float cooldownBoost;
    float fireBoost;
    float waterBoost;
    float airBoost;
    float natureBoost;

    public int dropChance;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = mold.capeSprite;
        capename = mold.capename;
        description = mold.description;
        healthBoost = mold.healthBoost;
        cooldownBoost = mold.cooldownBoost;
        fireBoost = mold.fireBoost;
        waterBoost = mold.waterBoost;
        airBoost = mold.airBoost;
        natureBoost = mold.natureBoost;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().TryAddItem(this.gameObject);
        }
    }
    public float GetHealthBoost()
    {
        return healthBoost;
    }
    public float GetCooldownBoost()
    {
        return cooldownBoost;
    }
    public float GetFireBoost()
    {
        return fireBoost;
    }
    public float GetWaterBoost()
    {
        return waterBoost;
    }
    public float GetAirBoost()
    {
        return airBoost;
    }
    public float GetNatureBoost()
    {
        return natureBoost;
    }

    public string GetItemInfo()
    {
        return mold.description;
    }
}
