using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCoins : MonoBehaviour
{
    TextMeshProUGUI coinsNb;
    void Start()
    {
        coinsNb = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    public void UpdateNbCoins(int coins)
    {
        coinsNb.text = coins.ToString();
    }
}
