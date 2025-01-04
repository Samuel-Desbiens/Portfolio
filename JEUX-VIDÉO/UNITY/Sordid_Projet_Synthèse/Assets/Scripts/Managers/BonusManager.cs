using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] GameObject potion;
    [SerializeField] GameObject powerBonus;
    [SerializeField] GameObject coin;
    private static int nbCoinToInit = 150;
    private static int nbPowerBonusToInit = 75;
    private static int nbPotionsToInit = 15;
    private GameObject[] potionArray = new GameObject[nbPotionsToInit];
    private GameObject[] powerBonusArray = new GameObject[nbPowerBonusToInit];
    private GameObject[] coinArray = new GameObject[nbCoinToInit];
    void Start()
    {
        GameObject BonusManager = new GameObject("BonusPool");
        DontDestroyOnLoad(BonusManager);
        InitCoins(BonusManager);
        InitPowers(BonusManager);
        InitPotions(BonusManager);
    }

    void InitCoins(GameObject manager)
    {
        for (int i = 0; i < nbCoinToInit; i++)
        {
            coinArray[i] = Instantiate(coin);
            coinArray[i].name += i;
            coinArray[i].transform.SetParent(manager.transform);
            coinArray[i].gameObject.SetActive(false);
        }
    }

    void InitPowers(GameObject manager)
    {
        for (int i = 0; i < nbPowerBonusToInit; i++)
        {
            powerBonusArray[i] = Instantiate(powerBonus);
            powerBonusArray[i].name += i;
            powerBonusArray[i].transform.SetParent(manager.transform);
            powerBonusArray[i].gameObject.SetActive(false);
        }
    }

    void InitPotions(GameObject manager)
    {
        for (int i = 0; i < nbPotionsToInit; i++)
        {
            potionArray[i] = Instantiate(potion);
            potionArray[i].name += i;
            potionArray[i].transform.SetParent(manager.transform);
            potionArray[i].gameObject.SetActive(false);
        }
    }

    public GameObject FindCoin()
    {
        foreach (GameObject coin in coinArray)
        {
            if (!coin.gameObject.activeSelf)
            {
                return coin;
            }
        }
        return null;
    }

    public GameObject FindPower()
    {
        foreach (GameObject power in powerBonusArray)
        {
            if (!power.gameObject.activeSelf)
            {
                return power;
            }
        }
        return null;
    }

    public GameObject FindPotion()
    {
        foreach (GameObject potion in potionArray)
        {
            if (!potion.gameObject.activeSelf)
            {
                return potion;
            }
        }
        return null;
    }
}
