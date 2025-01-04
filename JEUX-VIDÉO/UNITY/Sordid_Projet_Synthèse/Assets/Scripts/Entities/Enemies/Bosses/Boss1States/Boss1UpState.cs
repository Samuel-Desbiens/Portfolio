using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Boss1UpState : Boss1State
{

  bool moving = true;
  bool up = false;
  bool jumping = false;
  bool changingState = false;
  Timer upStateTime;
  [SerializeField] float upStateDuration = 30f;
  Timer attackTimer;
  [SerializeField] float attackCooldown = 3.0f;
  [SerializeField] float speed = 3;
  float jumpAnimFlip = 1.2f;
  float cellingHeight = -37.2f;
  const float meleeAttackAnimationCooldown = 0.5f;


  public override void Action()
  {
    animator.SetBool("moving", moving);
    upStateTime.Update();
    attackTimer.Update();
    if (changingState) return;
    if (jumping) return;
    if (moving)
    {
      Move();
    }
    else if (!up)
    {
      up = true;
      jumping = true;
      StartCoroutine(JumpUp(new Vector2(transform.position.x, cellingHeight), false));
    }
    else if (attackTimer.CanDo())
    {
      attackTimer.Reset();
      FacePlayerOnce();
      manager.SetLightsOn(false);
      StartCoroutine(Attack());
    }
  }

  private IEnumerator Attack()
  {
    animator.SetTrigger("melee");
    yield return new WaitForSeconds(meleeAttackAnimationCooldown);
    soundManager.PlayAudio(soundManager.wallHitClip, transform.position);
    if (facing == Side.LEFT)
    {
      attackZones[0].SetActive(true);
    }
    else
    {
      attackZones[1].SetActive(true);
    }
    manager.SetLightsOn(true);
  }

  public override void ManageStateChange()
  {
    if (upStateTime.CanDo() && !changingState)
    {
      animator.SetBool("moving", false);
      changingState = true;
      StartCoroutine(JumpUp(new Vector2(transform.position.x, startPos.y), true));
    }
  }
  void OnEnable()
  {

    cellingHeight = Physics2D.Raycast(transform.position, Vector2.up, 200, LayerMask.GetMask("Ground")).point.y;
    Debug.Log("Celling height" + cellingHeight);
    upStateTime = new(upStateDuration);
    upStateTime.Reset();
    attackTimer = new(attackCooldown);
    attackTimer.Reset();
  }
  void Move()
  {
    transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
    if (transform.position.x >= startPos.x - 1 || transform.position.x <= startPos.x + 1)
    {
      moving = false;
    }
  }

  IEnumerator JumpUp(Vector2 position, bool changeState)
  {
    animator.SetTrigger("jump");
    animator.SetTrigger("up");
    yield return new WaitForSeconds(jumpAnimFlip);
    transform.position = position;
    FlipSpriteY();
    yield return new WaitForSeconds(jumpAnimFlip);
    jumping = false;
    soundManager.PlayAudio(soundManager.dashClip, transform.position);
    if (changeState) manager.ChangeState(Boss1Manager.Boss1States.Shooting);
  }
  override protected void FacePlayer() { }
  void FacePlayerOnce()
  {
    if (player.transform.position.x < transform.position.x)
    {
      FlipSpriteX(true);
    }
    else if (player.transform.position.x > transform.position.x)
    {
      FlipSpriteX(false);
    }
  }

  void FlipSpriteX(bool flip)
  {
    Vector3 newScale = transform.localScale;
    if (flip)
    {
      facing = Side.LEFT;
    }
    else
    {
      facing = Side.RIGHT;
    }
    newScale.x = Mathf.Abs(newScale.x) * (int)facing;
    transform.localScale = newScale;
  }
  void FlipSpriteY()
  {
    Vector3 newScale = transform.localScale;
    newScale.y *= -1;
    transform.localScale = newScale;
  }
}
