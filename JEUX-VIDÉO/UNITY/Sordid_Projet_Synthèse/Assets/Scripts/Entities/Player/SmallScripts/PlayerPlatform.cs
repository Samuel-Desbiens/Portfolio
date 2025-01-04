using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D col;
    bool collisionWithPlatform = false;
    Collider2D platformCollider;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (collisionWithPlatform)
        {
            //TODO : replace with input system
            if (Input.GetKey(KeyCode.S))
            {
                platformCollider.GetComponent<Platform>().Disable(col);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collisionWithPlatform = true;
            platformCollider = collision.collider;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collisionWithPlatform = false;
            platformCollider = null;
        }
    }
}
