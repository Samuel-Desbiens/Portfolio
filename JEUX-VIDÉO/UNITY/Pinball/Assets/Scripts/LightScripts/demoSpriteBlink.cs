using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoSpriteBlink : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_mesh;
    [SerializeField] private Sprite[] text_sprite;
    [SerializeField] private float blinkspeed = 0.5f;
    [SerializeField] private float max_cycle = 4;
    private int bid = 0;

    void Start()
    {
        StartCoroutine(BlinkTarget());
    }

    IEnumerator BlinkTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkspeed);

            sprite_mesh.sprite = text_sprite[bid];

            if (++bid >= max_cycle) bid = 0;
        }
    }
}
