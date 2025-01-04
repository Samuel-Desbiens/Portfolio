using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoLetterCycle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprite_mesh;
    [SerializeField] private Sprite[] text_sprite;
    [SerializeField] private Sprite[] text_sprite_highlight;
    [SerializeField] private float blinkspeed = 0.5f;
    private int bid = 0;

    bool On;

    GameManager gameManagerScript;

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        On = false;
    }

    private void Update()
    {
        if(gameManagerScript.GetJackpotOn()  && !On)
        {
            StartCoroutine(CycleText());
            On = true;
        }
        else
        {
            if(On)
            {
                StopCoroutine(CycleText());
            }
        }
    }

    IEnumerator CycleText()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkspeed);

            for (int i = 0; i < text_sprite.Length; i++)
                sprite_mesh[i].sprite = text_sprite[i];

            sprite_mesh[bid].sprite = text_sprite_highlight[bid];

            if (++bid >= text_sprite.Length) bid = 0;
        }
    }
}
