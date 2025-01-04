using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneManager : MonoBehaviour
{
  [SerializeField] GameObject switchScenePortal;
  private void Start()
  {
    switchScenePortal.SetActive(false);
  }
  public void OnBossDeath()
  {
    switchScenePortal.SetActive(true);
  }
}
