using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerDeadState : NecromancerState
{

  const float deathAnimTime = 1.51f;
  private void OnEnable()
  {
    StartCoroutine(Death());
  }

  private IEnumerator Death()
  {
    animator.SetTrigger("dead");
    yield return new WaitForSeconds(deathAnimTime);
    gameObject.SetActive(false);
  }

  public override void Action()
  {
  }

  public override void ManageStateChange()
  {
  }

    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.Dead;
    }
}
