using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows;

public class SkullBehavior : FlyingEnemyBehavior
{
    //asd
    [SerializeField] int dmg = 8;
    Health playerHp;
  protected override IEnumerator StartAttack()
  {
        soundManager.PlayAudio(soundManager.magicAttackClip, transform.position);
        yield return new WaitForSeconds(attackTime);
        if (InAttackRange())
        {
            playerHp.TakeDmg(dmg);
        }
        yield return null;
  }

    private void Update()
    {
        RotateTowardDirection();
        CheckToAttack();
        CheckObstacles();
        CheckPlayer();
        Move();
    }
    #region CustomLifeCycleMethods

    protected override void Start()
    {
        playerHp = player.GetComponent<Health>();
        RollNewDirection();
        base.Start();
    }

    #endregion




}
