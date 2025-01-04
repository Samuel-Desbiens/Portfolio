using Harmony;
using System.Collections;
using UnityEngine;

public class NecromancerBigOrbState : NecromancerState
{

    float chargeTime = 2;
    bool orbFired = false;
    float fleeRange = 15f;

    
    private void Start()
    {
        soundManager = SoundManager.Instance;
        orbFired = false;
        ChargeAttack();
    }

    public override void Action()
    {
    }

    void ChargeAttack()
    {
        StartCoroutine(ChargeAttackCoroutine());
    }

    IEnumerator ChargeAttackCoroutine()
    {
        animator.SetTrigger("AttackOrb");
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(chargeTime);
        animator.SetBool("Charging", false);
        FireOrb();
    }

    void FireOrb()
    {
        soundManager.PlayAudio(soundManager.magicAttackClip, transform.position);
        NecromancerOrb orb = manager.GetOrb();
        orb.gameObject.SetActive(true);
        orb.SetTarget(player.transform);
        orb.transform.position = manager.firePoint.position;
        orbFired = true;
    }

    public override void ManageStateChange()
    {
        if (orbFired)
        {
            if(Vector2.Distance(player.transform.position, transform.position) < fleeRange)
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.Fleeing);
            }
            else
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.LaserGrid);
            }
        }
    }

    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.BigOrb;
    }
}
