using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1FrenzyState : Boss1State
{

  GameObject warningZone;

  float landMaxDamage = 25.0f;
  float landDmgDropOff = -3.0f;

  Timer attackCooldown;
  float attackCooldownTime = 1.5f;
  float groundHeight = -56.5f;

  float airTime = 1.0f;
  float reactTime = 1f;
  const float jumpAttackAnimationCooldown = 1.61f;
  const float landAttackAnimationCooldown = 0.2f;

  bool up = false;

  Timer frenzyStateTime;
  [SerializeField] float frenzyStateDuration = 25.0f;

  private void Start()
  {

    groundHeight = manager.GetGroundLvl();
    Debug.Log("Ground Pos  = " + groundHeight);

  }
  void OnEnable()
  {
    flipCooldown = 2;
    attackCooldown = new(attackCooldownTime + airTime + reactTime + jumpAttackAnimationCooldown + landAttackAnimationCooldown);
    warningZone = gameObject.Children()[0];
    frenzyStateTime = new(frenzyStateDuration);
    frenzyStateTime.Reset();
  }
  public override void ManageStateChange()
  {
    frenzyStateTime.Update();
    if (frenzyStateTime.CanDo() && !up)
    {
      manager.ChangeState(Boss1Manager.Boss1States.Shooting);
    }
  }

  public override void Action()
  {
    attackCooldown.Update();
    if (attackCooldown.CanDo())
    {
      manager.SetLightsOn(false);
      StartCoroutine(Attack());
      attackCooldown.Reset();
    }
  }

  private IEnumerator Attack()
  {
    up = true;
    //Jump Up
    animator.SetTrigger("jump");
    yield return new WaitForSeconds(jumpAttackAnimationCooldown);
    spriteRen.enabled = false;
    animator.enabled = false;
    boxTrigger.enabled = false;
    //In air disappear, get landing position and highlight it
    yield return new WaitForSeconds(airTime);
    transform.position = new Vector2(player.transform.position.x, groundHeight);
    warningZone.gameObject.SetActive(true);
    //Give time for player to react and land doing damage on landing
    yield return new WaitForSeconds(reactTime);
    warningZone.gameObject.SetActive(false);
    spriteRen.enabled = true;
    animator.enabled = true;
    animator.SetTrigger("land");
    yield return new WaitForSeconds(landAttackAnimationCooldown);
    boxTrigger.enabled = true;
    yield return new WaitForFixedUpdate();
    soundManager.PlayAudio(soundManager.wallHitClip, transform.position);
    up = false;
    if (boxTrigger.IsTouching(player.GetComponent<CapsuleCollider2D>()))
    {
      Debug.Log(Vector2.Distance(player.transform.position, transform.position));
      float damage = landDmgDropOff * Vector2.Distance(player.transform.position, transform.position) + landMaxDamage;
      if (damage > 0)
      {
        player.gameObject.GetComponent<Health>().TakeDmg(damage);
      }
    }
    manager.SetLightsOn(true);
  }


}
