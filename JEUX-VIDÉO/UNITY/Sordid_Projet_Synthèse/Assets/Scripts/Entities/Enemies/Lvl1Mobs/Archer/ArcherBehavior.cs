using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows;

public class ArcherBehavior : GroundedEnemyBehavior
{
    private ProjectilePool pool;
    private Vector2 arrowDir;
    [SerializeField] Transform firePoint;




    protected override void Start()
    {
        pool = GameObject.Find("ArcherSkeletonPool").GetComponent<ProjectilePool>();
        attackTime = 1.2f;
        base.Start();
    }


    void Update()
    {
        CheckLoS();
        CheckToAttack();
        CheckWalls();
        Move();
    }
    protected override IEnumerator StartAttack()
    {
        FlipSprite(speed);

        arrowDir = (player.position - firePoint.position).normalized;
        yield return new WaitForSeconds(attackTime);
        soundManager.PlayAudio(soundManager.bowAttackClip, gameObject.transform.position);
        ShootArrow();
    }

    private void ShootArrow()
    {
        GameObject arrow = pool.GetProjectile();
        if (arrow != null)
        {
            if (arrowDir.x > 0)
            {
                arrow.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                arrow.GetComponent<SpriteRenderer>().flipX = true;
            }
            arrow.SetActive(true);
            arrow.GetComponent<Projectile>().Shoot(arrowDir);
            arrow.transform.position = firePoint.position;
        }
    }
}
