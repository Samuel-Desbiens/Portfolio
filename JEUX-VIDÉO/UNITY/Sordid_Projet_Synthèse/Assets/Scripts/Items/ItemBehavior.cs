using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField] int price = 100;
    [SerializeField] UnityEvent onBuy;

    public void Buy()
    {
        onBuy.Invoke();
        gameObject.SetActive(false);
    }

    public int GetPrice()
    {
        return price;
    }
}
