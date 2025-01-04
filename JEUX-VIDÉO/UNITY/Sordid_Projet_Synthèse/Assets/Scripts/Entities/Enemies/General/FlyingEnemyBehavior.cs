using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEnemyBehavior : Enemy
{
  protected SpriteRenderer sprite;
  protected Vector2 currentDirection = Vector2.zero;



  protected override void Awake()
  {
    sprite = GetComponent<SpriteRenderer>();
    base.Awake();
  }

  private protected void RollNewDirection()
  {
    do
    {
      currentDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    } while (currentDirection == Vector2.zero);
  }
  protected void CheckObstacles()
  {
    RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, 0.2f * transform.localScale.x, lm);
    if (hit.collider && !hit.collider.GetComponent<PlatformEffector2D>())
    {
      RollNewDirection();
    }
  }
  protected void RotateTowardDirection()
  {
    float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    float z = NormalizeAngle(transform.rotation.eulerAngles.z);
    if (Mathf.Abs(z) > 90)
    {
      sprite.flipY = true;
    }
    else
    {
      sprite.flipY = false;
    }

  }
  float NormalizeAngle(float angle)
  {
    if (angle > 180f)
    {
      return angle - 360f;
    }
    else if (angle < -180f)
    {
      return angle + 360f;
    }
    else
    {
      return angle;
    }
  }

  protected virtual void Move()
  {
    transform.Translate(Vector2.right * speed * Time.deltaTime);
    animator.SetBool("moving", true);
  }
  protected void CheckPlayer()
  {
    if (Vector2.Distance(transform.position, player.position) < losRange)
    {
      currentDirection = (player.position - transform.position).normalized;
    }
  }

}
