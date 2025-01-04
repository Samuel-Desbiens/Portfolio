using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 10;
    protected Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector2 dir)
    {
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }
}
