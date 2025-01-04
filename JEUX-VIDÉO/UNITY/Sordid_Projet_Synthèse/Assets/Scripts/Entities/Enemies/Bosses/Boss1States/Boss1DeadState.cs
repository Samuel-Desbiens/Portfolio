using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1DeadState : Boss1State
{

  const float deathAnimTime = 3.02f;
  private Color hitColor = new(1, 0.5f, 0.5f, 0.5f);
  private void OnEnable()
  {
    manager.SetLightsOn(false);
    StartCoroutine(Death());
  }

  private IEnumerator Death()
  {
    var spriteRenderer = GetComponent<SpriteRenderer>();
    animator.SetTrigger("dead");
    for (int i = 0; i < 4; i++)
    {
      spriteRenderer.color = hitColor;
      yield return new WaitForSeconds(deathAnimTime / 8);
      spriteRenderer.color = new(1, 1, 1, 1);
      yield return new WaitForSeconds(deathAnimTime / 8);
    }
    gameObject.SetActive(false);
  }

  public override void Action()
  {
  }

  public override void ManageStateChange()
  {
  }
}
