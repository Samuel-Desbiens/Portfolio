using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularAirSlashUpgradeBehaviour : MonoBehaviour
{
    private AirSlashUpgradeRotation Rotation;

    private AirSlashUpgradeBehaviour ParentScript;

    private Vector3 BaseLocalPosition;

    private float Angle;
    private float RemainingRange = 0;

    Vector3 FiringPos;

    private bool once; //Vue que l'angle est donné après le cast ...

    private void Awake()
    {
        Rotation = transform.GetChild(0).GetComponent<AirSlashUpgradeRotation>();

        ParentScript = transform.parent.gameObject.GetComponent<AirSlashUpgradeBehaviour>();

        BaseLocalPosition = transform.localPosition;
    }
    private void OnEnable()
    {
        ResetSpell();
        this.Cast();
    }

    private void ResetSpell()
    {
        transform.localPosition = BaseLocalPosition;

        once = false;
    }

    private void Cast()
    {
        FiringPos = transform.position;

        RemainingRange = ParentScript.GetRange();
    }

    void Update()
    {
        if(!once)
        {
            once = true;
            Rotation.SetRotation(FiringPos,new Vector3(FiringPos.x + Mathf.Cos(Angle),FiringPos.y + Mathf.Sin(Angle),0));
        }

        Vector3 OldPos = transform.position;

        Vector2 nextPoint = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));

        transform.Translate(nextPoint * ParentScript.GetMovementSpeed() * Time.deltaTime);

        float DistanceTraveled = Vector3.Distance(OldPos, transform.position);

        RemainingRange -= DistanceTraveled;

        if (RemainingRange <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collisionObject = collider.gameObject;

        if (collisionObject.CompareTag("Enemy"))
        {
            collisionObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
            Vector3 pos = transform.position;
            Vector3 ColPos = collider.transform.position;
            collisionObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockBack());
        }
        else if (collisionObject.CompareTag("Boss"))
        {
            collisionObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
        }
        gameObject.SetActive(false);
    }

    public void SetAngle(float angle)
    {
        Angle = angle;
    }
}
