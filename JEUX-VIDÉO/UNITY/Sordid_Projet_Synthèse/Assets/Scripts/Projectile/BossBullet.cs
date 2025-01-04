using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBullet : MonoBehaviour
{
  [SerializeField] private int damage;
  [SerializeField] private float speed;
  [SerializeField] private float homingStraightSecs = 1.0f;
  [SerializeField] private float lifeTime = 5.0f;
  private bool homing = false;
  private Transform target;
  void OnEnable()
  {
    StartCoroutine(Disable());
  }

  void Update()
  {
    if (homing)
    {
      transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    else
    {
      transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      col.gameObject.GetComponent<Health>().TakeDmg(damage);
      DisableProjectile();
    }
    else if (col.gameObject.CompareTag("Ground"))
    {
      DisableProjectile();
    }
  }

  public void SetTarget(Transform _target)
  {
    target = _target;
    StartCoroutine(SetHoming());
  }
  public void SetDirection(Quaternion direction)
  {
    transform.rotation = direction;
  }

  private IEnumerator Disable()
  {
    yield return new WaitForSeconds(lifeTime);
    DisableProjectile();
  }

  private IEnumerator SetHoming()
  {
    yield return new WaitForSeconds(homingStraightSecs);
    homing = true;
  }

  private void DisableProjectile()
  {
    homing = false;
    gameObject.SetActive(false);
  }

}
