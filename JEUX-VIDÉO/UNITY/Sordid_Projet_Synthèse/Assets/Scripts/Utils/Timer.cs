using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Timer
{
  private float cooldown;
  private float time;
  private bool canDo;

  public Timer(float cooldown)
  {
    this.cooldown = cooldown;
    this.time = 0;
    canDo = true;
  }

  public float GetTimeLeft()
  {
    return cooldown - time;
  }

  public void SetCooldown(float _cooldown)
  {
    cooldown = _cooldown;
  }

  public void Reset()
  {
    time = 0;
    canDo = false;
  }

  public bool CanDo()
  {
    return canDo;
  }

  public void Update()
  {
    time += Time.deltaTime;
    if (time >= cooldown)
    {
      time = cooldown;
      canDo = true;
    }
  }
}
