using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProjectile : MonoBehaviour
{
    [SerializeField] int dmg = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDmg(dmg);
        }
        gameObject.SetActive(false);

    }
}
