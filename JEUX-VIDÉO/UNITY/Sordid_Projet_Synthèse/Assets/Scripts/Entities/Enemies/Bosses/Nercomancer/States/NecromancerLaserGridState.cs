using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerLaserGridState : NecromancerState
{

    [SerializeField] float gapBetweenLasers = 10f;
    [SerializeField] int nbTotalCycles = 5;
    [SerializeField] float laserDuration = 1.5f;
    [SerializeField] float laserGraceTime = 0.5f;
    [SerializeField] float warningTime = 0.75f;
    List<Laser> lasersComp = new();

    bool attackDone = false;
    float timeBetweenWaves => laserDuration + laserGraceTime + warningTime;

    private void Start()
    {
        attackDone = false;
        soundManager = SoundManager.Instance;
        SetLaserComp(manager.lasers);
        CreateLasers();
    }

    void SetLaserComp(List<LineRenderer> lasers)
    {
        for (int i = 0; i < lasers.Count; i++)
        {
            lasersComp.Add(lasers[i].GetComponent<Laser>());
        }

    }

    void CreateLasers()
    {
        animator.SetTrigger("AttackLaser");
        StartCoroutine(CreateLasersCoroutine());
    }

    IEnumerator CreateLasersCoroutine()
    {
        yield return new WaitForFixedUpdate();
        Vector2 dir = Vector2.right;
        for (int i = 0; i < nbTotalCycles; i++)
        {
            StartCoroutine(OneLaserCycle(dir));
            dir = Vector2.one - dir;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        attackDone = true;
    }


    void ChangeLaserState(List<LineRenderer> lasers, bool state)
    {
        for (int i = 0; i < lasers.Count; i++)
        {
            lasers[i].gameObject.SetActive(state);
        }
    }

    IEnumerator OneLaserCycle(Vector2 dir)
    {
        Vector2 pseudoRandomPos = new(transform.position.x + Random.Range(-3, 4), manager.fixedLaserYPos + Random.Range(-3, 4));

        ChangeLaserState(manager.warningLasers, true);
        SetUpLaserPos(pseudoRandomPos, manager.warningLasers, dir);
        soundManager.PlayAudio(soundManager.laserClip, transform.position);
        yield return new WaitForSeconds(warningTime);
        ChangeLaserState(manager.warningLasers, false);
        ChangeLaserState(manager.lasers, true);
        SetUpLaserPos(pseudoRandomPos, manager.lasers, dir);
        SetUpLaserColliders(manager.lasers);
        yield return new WaitForSeconds(laserDuration);
        ChangeLaserState(manager.lasers, false);

    }

    void SetUpLaserColliders(List<LineRenderer> lasers)
    {
        for (int i = 0; i < lasers.Count; i++)
        {
            lasersComp[i].SetEdgeCollider(lasers[i]);
        }
    }

    void SetUpLaserPos( Vector2 pos, List<LineRenderer> lasers, Vector2 dir)
    {
        Vector2 firstLaserPos = new Vector2(pos.x - (gapBetweenLasers * ((float)lasers.Count / 2) * dir.y), pos.y - (gapBetweenLasers * ((float)lasers.Count / 2) * dir.x));
        Vector2 currentLaserPos = firstLaserPos;

        LayerMask groundLayer = LayerMask.GetMask("Ground");

        for (int i = 0; i < lasers.Count; i++)
        {
            RaycastHit2D positiveHit = Physics2D.Raycast(currentLaserPos, dir, 200, groundLayer);
            RaycastHit2D negativeHit = Physics2D.Raycast(currentLaserPos, -dir, 200, groundLayer);

            Debug.DrawLine(currentLaserPos, currentLaserPos + (200 * -dir), Color.red, 2);
            Debug.DrawLine(currentLaserPos, currentLaserPos + (200 * dir), Color.blue, 2);

            if (positiveHit.collider && negativeHit.collider)
            {
                lasers[i].SetPosition(0, positiveHit.point);
                lasers[i].SetPosition(1, negativeHit.point);
            }
            else
            {
                lasers[i].gameObject.SetActive(false);
            }
            currentLaserPos += ((Vector2.one - dir) * gapBetweenLasers);
        }
    }


    public override void Action()
    {
    }

    public override void ManageStateChange()
    {
        if (attackDone)
        {
            if(manager.lastState == NecromancerManager.NecromancerStates.BigOrb)
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.Stunned);
            }
            else
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.Fleeing);
            }
        }
    }

    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.LaserGrid;
    }
}
