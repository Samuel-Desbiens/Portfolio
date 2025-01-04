using Harmony;
using System.Collections;
using UnityEngine;

public class Boss1ShootingState : Boss1State
{
  float speed = 3;
  float nextStagePercInc = 20;
  private bool shooting = false;

  //Projectiles
  ProjectileManager projectileManager;
  int topProjAmount = 8;
  int fowardProjAmount = 3;

  //Targeting
  float targetingAngleDegres = 22;
  float targetingAngleBottomOffset = 15;
  private float targetingAngleRatio;
  float playerDistanceTargeting = 45;


  //Cooldown
  Timer attackCooldown;
  float attackCooldownTime = 2.42f;

  //Attacks
  private float meleeRange = 10.0f;
  private float meleeDmg = 15.0f;
  const float upAttackAnimationCooldown = 0.7f;
  const float fowardAttackAnimationCooldown = 0.6f;
  const float meleeAttackAnimationCooldown = 0.5f;

  private void OnEnable()
  {
    flipCooldown = 0.5f;
    targetingAngleRatio = Mathf.Tan(targetingAngleDegres * Mathf.Deg2Rad); ;
    attackCooldown = new(attackCooldownTime);
    projectileManager = Finder.FindWithTag<ProjectileManager>("GameController");
    manager.ReduceNextStagePerc(nextStagePercInc);
  }

  public override void Action()
  {
    animator.SetBool("moving", false);
    if (!shooting && manager.IsDoorsClosed())
    {
      if (attackCooldown.CanDo())
      {
        manager.SetLightsOn(false);
        shooting = true;
        attackCooldown.Reset();
        SelectAttack();
      }
      else
      {
        attackCooldown.Update();
        animator.SetBool("moving", true);
        Move();
      }
    }
  }

  public override void ManageStateChange()
  {
    if (manager.GetHPRatio() * 100 < 50 && !manager.IsWentUp())
    {
      manager.WentUp();
      animator.SetBool("moving", false);
      manager.ChangeState(Boss1Manager.Boss1States.Up);
    }
    else if (manager.GetHPRatio() * 100 < manager.GetNextStageHealthPerc())
    {
      animator.SetBool("moving", false);
      manager.ChangeState(Boss1Manager.Boss1States.Frenzy);
    }
  }

  private void Move()
  {
    transform.Translate(Vector2.right * (player.transform.position - transform.position).normalized * speed * Time.deltaTime);
  }

  private void SelectAttack()
  {
    Vector2 diff = player.transform.position - transform.position;
    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= meleeRange &&
      Mathf.Abs(player.transform.position.y - transform.position.y) <= meleeRange / 2)
    {
      StartCoroutine(AttackMelee());
    }
    else if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x) * targetingAngleRatio)
    {
      animator.SetTrigger("buff");
      StartCoroutine(AttackUp());
    }
    else
    {
      animator.SetTrigger("spit");
      StartCoroutine(AttackFoward());
    }
  }

  private IEnumerator AttackUp()
  {
    animator.SetTrigger("buff");
    yield return new WaitForSeconds(upAttackAnimationCooldown);
    float startAngle = 180 - targetingAngleDegres;
    float angleDegOffset = (180 - targetingAngleDegres * 2) / topProjAmount;
    for (int i = 0; i < topProjAmount; i++)
    {
      ShootHomingProjectile(startAngle - (angleDegOffset * i + angleDegOffset / 2), player.transform);
    }
    shooting = false;
    manager.SetLightsOn(true);
  }

  private IEnumerator AttackFoward()
  {
    animator.SetTrigger("spit");
    yield return new WaitForSeconds(fowardAttackAnimationCooldown);
    float angleDegOffset = (targetingAngleDegres + targetingAngleBottomOffset) / fowardProjAmount;
    float startAngle = targetingAngleDegres * (int)IsFacing();
    for (int i = 0; i < fowardProjAmount; i++)
    {
      if (IsFacing() == Side.RIGHT)
      {
        ShootProjectile(startAngle - (angleDegOffset * i));
      }
      else
      {
        ShootProjectile((startAngle + 180) + (angleDegOffset * i));
      }

    }
    shooting = false;
    manager.SetLightsOn(true);
  }

  private IEnumerator AttackMelee()
  {
    animator.SetTrigger("melee");
    yield return new WaitForSeconds(meleeAttackAnimationCooldown);
    if (capsuleTrigger.IsTouching(player.GetComponent<CapsuleCollider2D>()))
    {
      player.GetComponent<Health>().TakeDmg(meleeDmg);
    }
    shooting = false;
    manager.SetLightsOn(true);
  }

  private void ShootProjectile(float angleDeg)
  {
    BossBullet proj = projectileManager.findBoss1Bullet();
    if (proj != null)
    {
      proj.transform.position = transform.position + (Vector3.right * (int)IsFacing() * 8) + Vector3.up * 2;
      proj.SetDirection(Quaternion.Euler(0, 0, angleDeg));
      proj.gameObject.SetActive(true);
      soundManager.PlayAudio(soundManager.magicAttackClip, transform.position);
    }
  }

  private void ShootHomingProjectile(float angleDeg, Transform target)
  {
    BossBullet proj = projectileManager.findBoss1Bullet();
    if (proj != null)
    {
      proj.transform.position = transform.position + Vector3.up * 4;
      proj.gameObject.SetActive(true);
      proj.SetTarget(target);
      proj.SetDirection(Quaternion.Euler(0, 0, angleDeg));
    }
  }

  public void SetProjectileManager(ProjectileManager pM)
  {
    projectileManager = pM;
  }
}
