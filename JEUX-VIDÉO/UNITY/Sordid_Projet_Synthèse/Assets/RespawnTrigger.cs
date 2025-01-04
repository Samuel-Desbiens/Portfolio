using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = spawnPoint.position;
    }
}
