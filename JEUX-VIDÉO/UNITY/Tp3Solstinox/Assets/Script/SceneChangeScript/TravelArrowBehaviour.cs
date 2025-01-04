using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelArrowBehaviour : MonoBehaviour
{
    [SerializeField] string AssociatedScene;

    [SerializeField] Direction transfert;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (AssociatedScene != null)
            {
                collision.GetComponent<PlayerController>().SetSceneDirection(transfert);

                SceneManager.LoadScene(AssociatedScene);
            }
            
        }
    }
}
