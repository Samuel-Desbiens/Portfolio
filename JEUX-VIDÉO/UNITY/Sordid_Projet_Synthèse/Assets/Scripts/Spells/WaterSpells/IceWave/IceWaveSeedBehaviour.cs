using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWaveSeedBehaviour : MonoBehaviour
{

    private IceWaveBehaviour ParentScript;

    private void Awake()
    {
        ParentScript = transform.parent.GetComponent<IceWaveBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.transform.tag != "Enemy") //Doit être update avec d'autre chose qui pourrait collide avec.
        {
            ParentScript.ChangeState(true);
            //gameObject.SetActive(false);
        }
    }
}
