using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerStunnedState : NecromancerState
{
    [SerializeField] float stunTime = 5f;
    Timer stunTimer;
    private void Start()
    {
        animator.SetTrigger("Stun");
        stunTimer = new Timer(stunTime);
        stunTimer.Reset();
    }
    public override void Action()
    {
    }

    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.Stunned;
    }

    public override void ManageStateChange()
    {
        stunTimer.Update();
        if (stunTimer.CanDo())
        {
            if(Random.Range(0,2) == 0)
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.Summoning);
            }
            else
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.BigOrb);
            }
        }
    }
}
