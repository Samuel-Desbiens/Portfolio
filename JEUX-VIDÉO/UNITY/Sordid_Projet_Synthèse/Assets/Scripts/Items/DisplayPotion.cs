using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPotion : MonoBehaviour
{
    TextMeshProUGUI potionNb;
    void Start()
    {
       potionNb = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    public void UpdateNbPotion(int potions)
    {
        potionNb.text = potions.ToString();
    }
   
}
