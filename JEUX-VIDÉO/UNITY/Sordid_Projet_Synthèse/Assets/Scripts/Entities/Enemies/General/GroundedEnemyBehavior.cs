using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GroundedEnemyBehavior : Enemy
{
    [SerializeField] float maxJumpDistance = 8f;
    [SerializeField] float jumpBoost = 1.75f;
    [SerializeField] float minJumpDistance = 3f;
    [SerializeField] Transform feets;
    protected Collider2D col;
    float maxFallDistance;



    //Movement
    bool blocked = false;
    // TODO : Set to Timer
    [SerializeField] float waitTime;
    float waitTimer = 0.0f;
    [SerializeField] float jumpCd = 0.25f;
    float jumpTimer = 0;



    //Constitution
    bool airborne = false;

    protected override void Awake()
    {
        maxFallDistance = maxJumpDistance * 1.5f;
        col = GetComponent<Collider2D>();
        base.Awake();
    }



    protected void Fall(float xOffset)
    {

        Vector2 offset = new(xOffset, -1);
        Debug.DrawLine(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(transform.position.x + offset.x, transform.position.y + offset.y - maxFallDistance), Color.red, 0.03f);
        RaycastHit2D floorBeneath = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.down, maxFallDistance, lm);
        if (floorBeneath.collider == null)
        {
            blocked = true;
        }
    }

    public override void BasicAttack()
    {
        FlipSprite(Mathf.Abs(speed) * targetSign);
        base.BasicAttack();
    }

    protected void Jump(Vector2 target)
    {

        if (jumpTimer < Time.time)
        {
            rb.velocity = Vector2.zero;

            //Debug.Log("Jump" + target.x);

            Vector2 directionToPoint = (target - (Vector2)transform.position).normalized;
            directionToPoint = new Vector2(directionToPoint.x / 1.5f, directionToPoint.y);

            float distance = Vector2.Distance(transform.position, target) + minJumpDistance;
            if(distance > maxJumpDistance)
            {
                distance = maxJumpDistance;
            }
            float yAdjustment = 0.02f * distance;
            directionToPoint.y += yAdjustment;

            float randomForce = UnityEngine.Random.Range(distance/1.5f, distance);

            // Add force towards the point
            rb.AddForce(randomForce  * directionToPoint * jumpBoost, ForceMode2D.Impulse);


            airborne = true;
            jumpTimer = Time.time + jumpCd;
        }

    }

    //public void OnTeleportFinished()
    //{
    //    dead = false;
    //    enemyCollider.enabled = true;
    //}
    //protected void Teleport(Vector2 pos)
    //{
    //    Debug.Log("tp");
    //    animator.SetTrigger("dead");
    //    enemyCollider.enabled = false;
    //    dead = true;
    //    transform.position = pos;
    //    animator.SetTrigger("rebirth");
    //}
    //protected void TeleportAbove(Vector2 pos)
    //{
    //    Teleport(pos);
    //}

    //protected void TeleportToSpawn()
    //{

    //}
    //protected void CheckTeleport()
    //{
    //    RaycastHit2D floorAbove = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, teleportRange, lm);
    //    if (floorAbove.collider != null)
    //    {
    //        Vector2 above = new Vector2(floorAbove.point.x, floorAbove.point.y + 5);
    //        RaycastHit2D isEmpty = Physics2D.Raycast(above, Vector2.up, 1, lm);
    //        if (isEmpty.collider == null)
    //        {
    //            TeleportAbove(above);
    //        }
    //    }

    //}
    protected void CheckJump(Vector2 offset)
    {

        Debug.DrawLine(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y ), new Vector2(transform.position.x + offset.x + (offset.x * maxJumpDistance), transform.position.y + offset.y + (offset.y * maxJumpDistance)), Color.blue, 0.03f);
        RaycastHit2D floorAngled = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), new Vector2(offset.x, offset.y), maxJumpDistance, lm);


        if(floorAngled.collider != null)
        {
            Jump(floorAngled.point);
        }
        else if(offset.y<0)
        {
            Fall(offset.x);
        }

    }

    protected void CheckWalls()
    {
        float dir = Mathf.Sign(speed);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0.5f, -1, 0), Color.red, 0.03f);
        Vector2 ajustedCenterPosition = new Vector2(transform.position.x, transform.position.y - 1);
        if (!blocked)
        {
            RaycastHit2D hitD = Physics2D.Raycast(ajustedCenterPosition, new Vector2(dir, 0), 0.2f * transform.localScale.x, lm);
            RaycastHit2D hitBD = Physics2D.Raycast(ajustedCenterPosition, new Vector2(dir, -1), 0.5f * transform.localScale.y, lm);
            RaycastHit2D hitTD = Physics2D.Raycast(ajustedCenterPosition, new Vector2(dir, 1), 0.5f * transform.localScale.y, lm);
            RaycastHit2D hitT = Physics2D.Raycast(ajustedCenterPosition, new Vector2(0, 1), maxFallDistance, lm);

            if (!targeting && !airborne)
            {
                //Debug.Log(hitT.collider);
                if (hitBD.collider == null)
                {
                    CheckJump(new Vector2(Mathf.Sign(speed), -1));
                }
                else if (hitT.collider && hitT.collider.gameObject.CompareTag("Platform"))
                {
                    Jump(new Vector2(hitT.point.x, hitT.point.y + maxFallDistance * 2));
                }
                else if (hitTD.collider == null)
                {
                    CheckJump(new Vector2(Mathf.Sign(speed), 1));
                }

            }
            if (hitD.collider != null)
            {
                blocked = true;
            }


        }
    }
    protected void Move()
    {
        animator.SetBool("moving", false);
        if (!blocked && !targeting && !dead)
        {
            FlipSprite(speed);
            animator.SetBool("moving", true);
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
        else if (blocked && !dead)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                speed = speed * -1;
                blocked = false;
                waitTimer = 0;

            }
        }
        else if (!blocked && targeting && !dead && !attacking)
        {

            FlipSprite(Mathf.Abs(speed) * targetSign);
            animator.SetBool("moving", true);
            transform.Translate(new Vector2(Mathf.Abs(speed) * targetSign * Time.deltaTime, 0));
            if (!airborne)
            {
                Jump(player.position);
            }
            RaycastHit2D hitB = Physics2D.Raycast(feets.position, Vector3.down, 0.2f * transform.localScale.x, lm);
            float yDir = (feets.position - player.position).normalized.y;
            if (hitB.collider != null && hitB.collider.gameObject.CompareTag("Platform") && yDir > 0)
            {
                hitB.collider.GetComponent<Platform>().Disable(col);
            }
        }
    }

    protected void PlayerDetected()
    {
        targeting = true;
        Debug.DrawLine(transform.position, player.position, Color.red, 0.03f);
        float disX = player.position.x - transform.position.x;
        targetSign = disX / Mathf.Abs(disX);
    }

    protected void CheckLoS()
    {
        float dis = Vector2.Distance(transform.position, player.position);
        if (dis < losRange)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position), dis, lm);

            if (hit.collider == null)
            {
                PlayerDetected();
            }
            else if (hit.collider.gameObject.CompareTag("Platform"))
            {
                hit = Physics2D.Raycast(hit.point + (Vector2)(player.position - transform.position).normalized, player.position - transform.position, dis, lm);
                if (hit.collider == null)
                {
                    PlayerDetected();
                }
            }
            else
            {
                targeting = false;
            }
        }

    }





    private void OnCollisionEnter2D(Collision2D collision)
    {

        RaycastHit2D hit = Physics2D.Raycast(feets.position, Vector2.down, 0.5f, lm);
        if (hit.collider)
        {
            airborne = false;
        }

    }


    protected void FlipSprite(float speed)
    {
        if (speed < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (speed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


}
