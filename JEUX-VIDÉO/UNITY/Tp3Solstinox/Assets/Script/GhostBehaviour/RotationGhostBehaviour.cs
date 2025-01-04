using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGhostBehaviour : MonoBehaviour
{
    [SerializeField] private Direction actualDirection;

    const float speed = 2f;

    float MinimumTimer;
    const float FullTimer = 0.25f;

    // Update is called once per frame
    void Update()
    { 
        if (actualDirection == Direction.Up)
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        else if(actualDirection == Direction.Down)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if(actualDirection == Direction.Left)
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
        if(!collision.gameObject.CompareTag("Weapon"))
        {
            if (MinimumTimer == 0)
            {
                if (actualDirection < Direction.Left)
                {
                    actualDirection++;
                }
                else
                {
                    actualDirection = 0;
                }
                MinimumTimer = FullTimer;
            }
        }
       
    }
}
