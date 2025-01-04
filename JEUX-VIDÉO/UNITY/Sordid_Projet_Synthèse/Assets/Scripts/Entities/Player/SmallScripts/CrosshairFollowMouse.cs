using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollowMouse : MonoBehaviour
{
    private const float ZModifier = 100;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, ZModifier);
    }
}
