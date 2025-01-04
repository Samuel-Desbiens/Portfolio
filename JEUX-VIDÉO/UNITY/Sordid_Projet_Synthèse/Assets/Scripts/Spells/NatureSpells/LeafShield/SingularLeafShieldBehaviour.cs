using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularLeafShieldBehaviour : MonoBehaviour
{
  LeafShieldBehaviour ParentScript;

  private void Awake()
  {
    ParentScript = transform.parent.GetComponent<LeafShieldBehaviour>();
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
        GameObject collisionObject = collision.gameObject;
    if (!collisionObject.CompareTag("Player"))
    {
      if (collisionObject.CompareTag("Enemy"))
      {
            collision.gameObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
            Vector3 pos = transform.position;
                Vector3 ColPos = collision.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockback());
      }
      else if (collisionObject.CompareTag("Boss"))
      {
                collisionObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
      }
      else if (collisionObject.CompareTag("EnemyProjectile"))//A changer avec le actual tag utiliser
      {
                collisionObject.SetActive(false); // Pourrait �tre g�rer diff�rement d�pendant de comment les projectiles ennemies sont g�rer
      }
      else if (ParentScript.GetThrown())
      {
          gameObject.SetActive(false);
      }
    }
   
  }
}
