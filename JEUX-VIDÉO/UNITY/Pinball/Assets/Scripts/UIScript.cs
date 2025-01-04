using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int[] infos = gameManagerScript.GetGameInfos();

        gameObject.GetComponent<Text>().text = infos[0] + "\n" + infos[1] + "\n\n" + infos[2];
    }
}
