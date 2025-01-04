using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows;

public class SpearmanBehavior : GroundedEnemyBehavior
{
    [SerializeField] private int damage = 12;
    Health playerHp;
  protected override IEnumerator StartAttack()
  {
    yield return new WaitForSeconds(attackTime);
    if (InAttackRange())
    {
            playerHp.TakeDmg(damage);
            soundManager.PlayAudio(soundManager.spearSkeletonClip, gameObject.transform.position);
    }
  }
    void Update()
    {
        CheckLoS();
        CheckToAttack();
        CheckWalls();
        Move();
    }

    #region CustomLifeCycleMethods

    protected override void Start()
    {
        playerHp = player.GetComponent<Health>();
        attackTime = 0.8f;
        base.Start();
    }
    #endregion



}
