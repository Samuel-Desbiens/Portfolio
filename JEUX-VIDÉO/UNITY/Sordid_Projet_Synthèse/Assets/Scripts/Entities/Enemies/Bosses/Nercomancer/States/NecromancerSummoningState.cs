using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSummoningState : NecromancerState
{
    [SerializeField] float enemySpawnDistanceFromBoss = 5;
    [SerializeField] float summoningTime = 4;
    [SerializeField] float summonB1HpRatio = 0.5f;
    [SerializeField] float minDistForShieldBubble = 10;

    bool doneSummoning = false;
    public override void Action()
    {
    }

    void Start()
    {
        doneSummoning = false;
        animator.SetTrigger("AttackOrb");
        animator.SetBool("Charging", true);
        if (manager.GetHPRatio() <= summonB1HpRatio && !manager.boss1Summoned)
        {
            SpawnBoss1();
        }
        else
        {
            SpawnMobs();
        }
    }
    void SpawnBoss1()
    {
        StartCoroutine(SpawnBoss1Coroutine());
    }

    IEnumerator SpawnBoss1Coroutine()
    {
        yield return new WaitForSeconds(summoningTime);

        manager.boss1.SetActive(true);

        RaycastHit2D summonPoint = Physics2D.Raycast(transform.position, Vector2.down, 200, LayerMask.GetMask("Ground"));
        if (summonPoint.collider)
        {
            manager.boss1.transform.position = summonPoint.point;
        }
        else
        {
            manager.boss1.transform.position = transform.position;
        }
        Boss1Manager boss1Manager = manager.boss1.GetComponent<Boss1Manager>();
        boss1Manager.ChangeState(Boss1Manager.Boss1States.Frenzy);
        manager.boss1Summoned = true;
        OnSummoningDone();
    }

    void OnSummoningDone()
    {
        animator.SetBool("Charging", false);
        doneSummoning = true;
    }

    void SpawnMobs()
    {
        StartCoroutine(SpawnMobsCoroutine());
    }

    IEnumerator SpawnMobsCoroutine()
    {
        yield return new WaitForSeconds(summoningTime);
        Vector2 rightPos = new Vector2(transform.position.x + enemySpawnDistanceFromBoss, transform.position.y);
        Vector2 leftPos = new Vector2(transform.position.x - enemySpawnDistanceFromBoss, transform.position.y);

        RaycastHit2D rightHit = Physics2D.Raycast(rightPos, Vector2.left);
        RaycastHit2D leftHit = Physics2D.Raycast(leftPos, Vector2.right);
        if (rightHit.collider)
        {
            rightPos = leftPos;
        }
        else if (leftHit.collider)
        {
            leftPos = rightPos;
        }
        manager.possibleSummons.Random().Spawn(rightPos, player.transform);
        manager.possibleSummons.Random().Spawn(leftPos, player.transform);
        OnSummoningDone();

    }
    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.Summoning;
    }

    public override void ManageStateChange()
    {
        if (doneSummoning)
        {
            if(Vector2.Distance(transform.position, player.transform.position) < minDistForShieldBubble)
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.BubbleShield);
            }
            else
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.BigOrb);
            }
        }
    }


}
