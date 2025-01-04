using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosionBehaviour : MonoBehaviour
{
    private const float maxStayTimer = 0.5f;
    private float timerLeft;

    private const float baseScale = 20f;

    private const float scaling = 1.012f;
    SoundManager soundManager;

    private GameObject parent;

    Animator animator;

    FireBallBehaviour ParentScript;

    Vector3 BaseLocalPosition; //Fix explosion portal


    private void Awake()
    {
        BaseLocalPosition = transform.localPosition;

        parent = transform.parent.gameObject;
        animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;

        ParentScript = transform.parent.GetComponent<FireBallBehaviour>();

    }
    private void OnEnable()
    {
        transform.localPosition = BaseLocalPosition;
        transform.localScale = new Vector3(baseScale, baseScale, baseScale);
        timerLeft = maxStayTimer;
        animator.SetTrigger("explode");
        soundManager.PlayAudio(soundManager.fireBallExplosionClip, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderObject = collider.gameObject;
        if (colliderObject.CompareTag("Enemy"))
        {
            colliderObject.GetComponent<Enemy>().TakeDmg(ParentScript.GetDamage(), ParentScript.GetElements());
            Vector3 pos = transform.position;
            Vector3 ColPos = collider.transform.position;
            colliderObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColPos.x - pos.x, ColPos.y - pos.y) * ParentScript.GetKnockback());
        }
        else if (colliderObject.CompareTag("Boss"))
        {
            colliderObject.GetComponent<Health>().TakeDmg(ParentScript.GetDamage());
        }
    }

    private void Update()
    {
        if (timerLeft <= 0)
        {
            parent.SetActive(false);
        }
        else
        {
            timerLeft -= Time.deltaTime;
            transform.localScale *= scaling;
        }
    }
}
