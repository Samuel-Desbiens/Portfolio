using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Compass : MonoBehaviour
{
    GameObject EndPortal;
    GameObject Player;
    Vector3 LastKnownPortalPosition;
    bool NewLevel = false;

    private void Start()
    {
        Player = GameObject.Find("Player");

        SceneManager.sceneLoaded += LoadScene;

        NewLevel = true;
    }
    private void LoadScene(Scene scene, LoadSceneMode mode)
    {
        NewLevel = true;
    }

    private void Update()
    {
        if(NewLevel)
        {
            EndScenePortal portal = FindFirstObjectByType<EndScenePortal>(FindObjectsInactive.Include);
            if(portal!= null)
            {
                EndPortal = portal.gameObject;
                NewLevel = false;
            }
        }
        if(EndPortal != null)
        {
            LastKnownPortalPosition = EndPortal.transform.position;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0,-(Mathf.Atan2(LastKnownPortalPosition.x - Player.transform.position.x, LastKnownPortalPosition.y - Player.transform.position.y) * Mathf.Rad2Deg)));
    }
}
