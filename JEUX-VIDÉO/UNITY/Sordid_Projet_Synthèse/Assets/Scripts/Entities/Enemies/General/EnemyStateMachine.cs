using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineBehaviour
{
  int count;
  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (stateInfo.IsName("Attack"))
    {
      animator.SetBool("attack", false);
    }
  }
}
