using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBehaviour : MonoBehaviour
{
    const int rotationSpeed = 500;
    int actualRotationSpeed;

    private void Start()
    {
        if(name.Contains("Ball"))
        {
            actualRotationSpeed = rotationSpeed / 2;
        }
        else
        {
            actualRotationSpeed = rotationSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, actualRotationSpeed * Time.deltaTime,0);
    }
}
