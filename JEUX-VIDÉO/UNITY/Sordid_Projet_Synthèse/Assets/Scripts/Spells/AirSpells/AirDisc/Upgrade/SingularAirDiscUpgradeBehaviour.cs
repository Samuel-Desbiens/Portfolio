using SuperTiled2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularAirDiscUpgradeBehaviour : MonoBehaviour
{
    private float DistanceLeftToTraveled =  0f;

    private float Angle;

    AirDiscUpgradeBehaviour ParentScript;

    private Vector3 BaseLocalPosition;

    bool Done;

    private void Awake()
    {
        ParentScript = transform.parent.gameObject.GetComponent<AirDiscUpgradeBehaviour>();
        BaseLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        this.ResetSpell();
        this.Cast();
    }

    private void ResetSpell()
    {
        DistanceLeftToTraveled = ParentScript.GetRange();

        transform.localPosition = BaseLocalPosition;

        Done = false;
    }

    private void Cast()
    {
        Vector3 FiringPos = transform.position;

        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Angle = Mathf.Atan2((MousePos - FiringPos).y, (MousePos - FiringPos).x);
    }

    void Update()
    {
        if (DistanceLeftToTraveled > 0)
        {
            Vector3 OldPos = transform.position;

            Vector2 NextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

            transform.Translate(NextPoint * ParentScript.GetMovementSpeed() * Time.deltaTime);

            DistanceLeftToTraveled -= Vector2.Distance(transform.position, OldPos);

            if (DistanceLeftToTraveled <= 0)
            {
                DistanceLeftToTraveled = 0;
            }
        }
        else
        {
            Done = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (!collisionObject.CompareTag("Player") && !collisionObject.CompareTag("Ground"))
        {
            if (collisionObject.CompareTag("Enemy"))
            {
                collisionObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
                Vector3 pos = transform.position;
                Vector3 ColPos = collision.transform.position;
                collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockBack());
            }
            else if (collisionObject.CompareTag("Boss"))
            {
                collisionObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public bool GetDone()
    {
        return Done;
    }
}
