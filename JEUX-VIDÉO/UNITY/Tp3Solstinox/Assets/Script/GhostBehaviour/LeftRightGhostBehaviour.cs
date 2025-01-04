using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightGhostBehaviour : MonoBehaviour
{
    bool goingLeft;

    const float speed = 2f;

    float MinimumTimer;
    const float FullTimer = 0.25f;

    private void Start()
    {
        goingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (goingLeft)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }

        if (MinimumTimer > 0)
        {
            MinimumTimer -= Time.deltaTime;
            if (MinimumTimer < 0)
            {
                MinimumTimer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Weapon"))
        {
            if (MinimumTimer == 0)
            {
                goingLeft = !goingLeft;
                MinimumTimer = FullTimer;
            }
        }
    }
}
