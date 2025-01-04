using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerOrb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 7;
    [SerializeField] int dmg = 10;

    Transform target;
    Rigidbody2D rb;
    Animator animator;
    Collider2D col;
    private void OnEnable()
    {
        Invoke(nameof(Explode), lifeTime);
    }
    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetColliderState(false);
            target.GetComponent<Health>().TakeDmg(dmg);
        }
        Explode();

    }
    public void SetColliderState(bool state)
    {
        col.enabled = state;
    }
    void Explode()
    {
        animator.SetTrigger("Explode");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardTarget();
    }
    void MoveTowardTarget()
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;
        rb.AddForce(speed * Time.deltaTime * dir, ForceMode2D.Force);
    }
}
