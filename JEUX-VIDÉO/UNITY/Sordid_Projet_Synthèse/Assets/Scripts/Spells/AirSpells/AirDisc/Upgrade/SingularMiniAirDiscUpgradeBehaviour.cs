using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularMiniAirDiscUpgradeBehaviour: MonoBehaviour
{
    private AirDiscUpgradeBehaviour ParentScript;

    private float Angle;

    private Vector3 BaseLocalPosition;

    private void Awake()
    {

        ParentScript = transform.parent.gameObject.GetComponent<AirDiscUpgradeBehaviour>();
        BaseLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        this.ResetSpell();
    }

    private void ResetSpell()
    {
        transform.localPosition = BaseLocalPosition;
    }

    void Update()
    {
        Vector3 PlayerPos = GameObject.Find("Player").transform.position;

        Angle = Mathf.Atan2((PlayerPos - transform.position).y, (PlayerPos - transform.position).x);

        Vector2 NextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

        transform.Translate(NextPoint * (ParentScript.GetMovementSpeed() - 2) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else if (collisionObject.CompareTag("Enemy"))
        {
            collisionObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage()/4, ParentScript.GetElements());
            Vector3 pos = transform.position;
            Vector3 ColPos = collision.transform.position;
            collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockBack()/ 5);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collisionObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage() / 4);
        }
        else if(!collisionObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
