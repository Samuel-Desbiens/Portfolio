using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class IceSlashRotationAndCollision : MonoBehaviour
{
    IceSlashBehaviour ParentScript;

    private void OnEnable()
    {
        ParentScript = transform.parent.GetComponent<IceSlashBehaviour>();
    }
    public void SetRotation(Vector3 FiringPos)
  {
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 + (-(Mathf.Atan2(FiringPos.x - transform.position.x, FiringPos.y - transform.position.y) * Mathf.Rad2Deg)))); //Fait en sorte que l'�p�e face away du joueur
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    GameObject collisionObject = collision.gameObject;
    //� Besoin d'�tre ici pour faire tourner la hitbox avec le visuel (ce qui explique les accessor weird) ...
    if (collisionObject.CompareTag("Enemy"))
    {
            collisionObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
            Vector3 pos = transform.position;
            Vector3 ColPos = collision.transform.position;
            collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockback());
    }

    else if (collisionObject.CompareTag("Boss"))
    {
            collisionObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
    }
  }
}
