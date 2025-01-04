using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Slime : Enemy
{
    // Start is called before the first frame update
    Light2D light2D;
    Health playerHp;
    [SerializeField] int dmg = 5;
    protected Vector2 dir = Vector2.right;
    protected SpriteRenderer sprite;
    bool onGround =false;

    #region CustomLifeCycleMethods

    protected override void Start()
    {
        onGround = false;
        light2D = GetComponentInChildren<Light2D>();
        playerHp = player.GetComponent<Health>();
        sprite = GetComponent<SpriteRenderer>();
        base.Start();
        Color color = new(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        light2D.color = color;
        sprite.color = color;
    }

    #endregion



    protected override IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackTime);
        playerHp.TakeDmg(dmg);
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            CheckToAttack();
            CheckEdge();
            CheckSide();
            Move();
        }
    }

    protected void CheckEdge()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + dir.x, transform.position.y), Vector2.down, 0.5f * transform.localScale.y, lm);
        if (!hit.collider)
        {
            dir = new Vector2(-dir.x, 0);
            sprite.flipX = (dir.x < 0);
        }
    }

    protected void CheckSide()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + dir.x, transform.position.y), dir, 0.5f * transform.localScale.y, lm);
        if (hit.collider)
        {
            dir = new Vector2(-dir.x, 0);
            sprite.flipX = (dir.x < 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    protected void Move()
    {
        animator.SetBool("moving", true);
        transform.Translate(speed * Time.deltaTime * dir);
    }
}
