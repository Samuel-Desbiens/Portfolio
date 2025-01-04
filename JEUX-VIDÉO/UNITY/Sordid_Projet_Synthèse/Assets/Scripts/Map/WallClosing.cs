using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClosing : MonoBehaviour
{
  [SerializeField] Vector2 targetPosition;
  [SerializeField] Vector2 startPosition;
  [SerializeField] float speed;

  public void CloseWall()
  {
    StartCoroutine(CloseWallCoroutine());
  }

  public void OpenWall() 
  { 
    StartCoroutine(OpenWallCoroutine());
  }


  IEnumerator CloseWallCoroutine()
  {
    yield return new WaitForFixedUpdate();
    while ((Vector2)transform.position != targetPosition)
    {
      transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
  }
  IEnumerator OpenWallCoroutine()
  {
    while ((Vector2)transform.position != startPosition)
    {
      yield return new WaitForFixedUpdate();
      transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
    }
  }
}
