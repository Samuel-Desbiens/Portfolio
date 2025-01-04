using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] TextMeshProUGUI textKills;
    [SerializeField] TextMeshProUGUI items;
    [SerializeField] TextMeshProUGUI xp;

    [SerializeField] Transform itemParent;
    ScoreManager score;

    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManager.instance;
        Debug.Log(score.nbItemsCollected);
        textCoins.text = score.coinsCollected.ToString();
        textKills.text = score.enemisKilled.ToString();
        items.text = score.nbItemsCollected.ToString();
        xp.text = score.xp.ToString();

        SetItemList();

    }
    void SetItemList()
    {
        GameObject[] allItems = score.GetList();

        for (int i = 0; i < allItems.Length; i++)
        {
            Sprite sprite = allItems[i].GetComponent<SpriteRenderer>().sprite;
            GameObject itemImage = itemParent.GetChild(i).gameObject;
            itemImage.SetActive(true);
            itemImage.GetComponent<Image>().sprite = sprite;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
