using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{

    Rigidbody rb;

    float StartWeight;
    float AddWeight;

    int UpdateHeld;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartWeight = rb.mass;
        AddWeight = 30;


        UpdateHeld = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("BallLauncher"))
        {
            if (UpdateHeld <= 100)
            {
                UpdateHeld++;

                rb.mass += AddWeight;
            }
        }
        else
        {
            UpdateHeld = 0;
            rb.mass = StartWeight;
        }
    }
}
