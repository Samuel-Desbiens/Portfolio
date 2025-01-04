using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGhostBehaviour : MonoBehaviour
{
    bool goingUp;

    const float speed = 2f;

    float MinimumTimer;
    const float FullTimer = 0.25f;

    private void Start()
    {
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp)
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if(MinimumTimer > 0)
        {
            MinimumTimer -= Time.deltaTime;
            if(MinimumTimer < 0)
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
                goingUp = !goingUp;
                MinimumTimer = FullTimer;
            }
        }
    }
}
