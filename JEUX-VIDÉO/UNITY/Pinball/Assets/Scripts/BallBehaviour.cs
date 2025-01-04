using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    GameManager gameManagerScript;

    Rigidbody rb;

    float BumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManagerScript.GetMainBallPos(gameObject.transform.position);

        BumpForce = 2500;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            gameManagerScript.LoseLife();
            Destroy(gameObject);
        }
          else if (collision.gameObject.CompareTag("Bumper"))
        {
            gameManagerScript.Addpoints(10);
            Vector3 BumpDir = -(collision.contacts[0].point - transform.position);
            rb.AddForce(BumpDir * BumpForce);
        }
    }
}
