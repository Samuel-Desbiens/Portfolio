using UnityEngine;

public class IndividualFireBoltUpgradeBehaviour : MonoBehaviour
{
    Vector2 Direction;

    Rigidbody2D RB;

    FireboltUpgradeBehaviour ParentScript;

    private void OnEnable()
    {

    }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        ParentScript = transform.parent.GetComponent<FireboltUpgradeBehaviour>(); ;
    }

    public void LaunchAndDirection(Vector2 directionForce)
    {
        Direction = directionForce;

        RB.AddForce(Direction * ParentScript.GetRange(), ForceMode2D.Impulse);
    }

    private void Update()
    {
        float angle = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderObject = collider.gameObject;
        if (collider.tag != "Player")
        {
            if (colliderObject.CompareTag("Enemy"))
            {
                colliderObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
            }
            else if (colliderObject.CompareTag("Boss"))
            {
                colliderObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
            }

            gameObject.SetActive(false);
        }


    }


}
