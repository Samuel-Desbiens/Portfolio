using Harmony;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkullMage : FlyingEnemyBehavior
{
    [SerializeField] Transform castSpellPos;
    [SerializeField] ProjectilePool pool;
    SoundManager soundManager;
    
    protected override IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackTime);
        GameObject projectile = pool.GetProjectile();
        Vector2 projDir = (player.position - transform.position).normalized;
        projectile.SetActive(true);
        projectile.transform.position = castSpellPos.position;
        projectile.GetComponent<Projectile>().Shoot(projDir);
        soundManager.PlayAudio(soundManager.magicAttackClip, transform.position);
        yield return null;
    }

    protected override void Move()
    {
        if (!attacking)
        {
            base.Move();
        }
    }


    private void Update()
    {
        RotateTowardDirection();
        CheckToAttack();
        CheckObstacles();
        CheckPlayer();
        Move();
    }


    protected override void Start()
    {
        pool = GameObject.Find("MageSkullPool").GetComponent<ProjectilePool>();
        soundManager = SoundManager.Instance;
        attackTime = 1.2f;
        base.Start();
    }
    protected override void OnEnable()
    {
        RollNewDirection();
        base.OnEnable();
    }





}
