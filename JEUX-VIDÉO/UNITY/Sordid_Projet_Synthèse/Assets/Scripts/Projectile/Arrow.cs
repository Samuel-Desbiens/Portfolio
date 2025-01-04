using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Arrow : Projectile
{
  [SerializeField] float timeStuck = 8;
  [SerializeField] private int dmg = 8;
  Collider2D col;
  Transform baseParent;
  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<Collider2D>();
  }

  private void Start()
  {
    baseParent = transform.parent;
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      collision.gameObject.GetComponent<Health>().TakeDmg(dmg);
    }
    rb.simulated = false;
    transform.parent = collision.transform;
    col.enabled = false;
    StartCoroutine(Dispawn());
  }

  IEnumerator Dispawn()
  {
    yield return new WaitForSeconds(timeStuck);
    gameObject.SetActive(false);
    col.enabled = true;
    rb.simulated = true;
    transform.parent = baseParent;

  }


}
