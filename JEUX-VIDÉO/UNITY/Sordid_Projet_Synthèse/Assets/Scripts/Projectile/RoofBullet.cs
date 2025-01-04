using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class RoofBullet : MonoBehaviour
{
  [SerializeField] private int damage;
  [SerializeField] private float speed;

  void Update()
  {
    transform.Translate(Vector3.right*Time.deltaTime*speed);
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      col.gameObject.GetComponent<Health>().TakeDmg(damage);
      gameObject.SetActive(false);
    } else if (col.gameObject.CompareTag("Ground"))
    {
      gameObject.SetActive(false);
    }
  }
  public void SetDirection(Quaternion direction)
  {
    transform.rotation = direction;
  }
}
