using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableRangeCalculator : MonoBehaviour
{
    [SerializeField] string lightTag = "Light";
    [SerializeField] float range = 50;
    [SerializeField] float timeBetweenUpdates = 3;
    float nextUpdateTime = 0;
    GameObject[] allObjs;

  private void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    allObjs = Array.Empty<GameObject>();
    StartCoroutine(GetAllObjs());
  }

  IEnumerator GetAllObjs()
  {
    for (int i = 0; i < 60; i++)
    {
      if (allObjs == null || allObjs.Length == 0)
      {
        allObjs = GameObject.FindGameObjectsWithTag(lightTag);
        yield return new WaitForSeconds(1f);
      }
      else
      {
        break;
      }
    }


  }



  void Update()
  {

    if (nextUpdateTime < Time.time)
    {
      UpdateLights();
    }
  }


  void UpdateLights()
  {
      if(allObjs != null)
      {
          for (int i = 0; i < allObjs.Length; i++)
          {
              if (Vector2.Distance(allObjs[i].transform.position, transform.position) < range)
              {
                  allObjs[i].SetActive(true);


              }
              else
              {
                  allObjs[i].SetActive(false);
              }
          }
            
      }
      nextUpdateTime = Time.time + timeBetweenUpdates;

  }
}
