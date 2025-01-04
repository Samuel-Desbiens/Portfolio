using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofProjZone : MonoBehaviour
{
  ProjectileManager pm;
  GameObject[] positions;
  float attackAmount = 15;
  float attackCooldown = 0.3f;
  void Start()
  {
    positions = gameObject.Children();
    pm = Finder.FindWithTag<ProjectileManager>("GameController");
  }

  private void OnEnable()
  {
    StartCoroutine(Attack());
  }

  IEnumerator Attack()
  {
    for (int i = 0; i < attackAmount; i++)
    {
      yield return new WaitForSeconds(attackCooldown);
      RoofBullet proj = pm.findBoss1RoofBullet();
      if (proj != null)
      {
        proj.transform.position = positions.Random().transform.position;
        proj.gameObject.SetActive(true);
      }
    }
    gameObject.SetActive(false);
  }
}
