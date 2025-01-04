using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWaveMiniWaveBehaviour : MonoBehaviour
{
    private IceWaveBehaviour ParentScript;
    private SpriteRenderer Sprite;

    private bool MiniWaveDone;

    private Vector3 BaseBaseScale = new Vector3(15f, 30f, 0);
    private float WaveScaleDownMultiplier = 0.90f;

    Rigidbody2D RB;
    private void Awake()
    {
        ParentScript = transform.parent.GetComponent<IceWaveBehaviour>();
        Sprite = gameObject.GetComponent<SpriteRenderer>();

        RB = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ResetSpell();
        Cast();
    }

    private void ResetSpell()
    {  
        transform.localScale = BaseBaseScale;

        MiniWaveDone = false;
    }

    private void Cast()
    {
        //Logique Inverse
        if (ParentScript.GetDirection() < 0)
        {
            Sprite.flipX = false;
        }
        else
        {
            Sprite.flipX = true;
        }
    }

    private void Update()
    {
        if (!MiniWaveDone)
        {
            transform.Translate(new Vector3(ParentScript.GetMovementSpeed()/2 * -ParentScript.GetDirection(), 0, 0) * Time.deltaTime);

            if (RB.velocity.y < -3)
            {
                MiniWaveDone = true;

                GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(ParentScript.GetMovementSpeed() / 2 * -ParentScript.GetDirection(), 0, 0) * Time.deltaTime);

            transform.localScale *= WaveScaleDownMultiplier;

            if (transform.localScale.x <= 0.1f || transform.localScale.y <= 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDmg(transform.parent.GetComponent<IceWaveBehaviour>().GetDamage()/2);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y) * transform.parent.GetComponent<IceWaveBehaviour>().GetKnockback());
        }
        else if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Health>().TakeDmg(transform.parent.GetComponent<IceWaveBehaviour>().GetDamage()/2);
        }
        else if (collision.tag != "Player")
        {
            float CollisionAngle = Vector3.Angle(collision.transform.position, Vector3.up);

            if (CollisionAngle > 70 && CollisionAngle < 105) //Vérifie si la collision est de coté (un mur)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
