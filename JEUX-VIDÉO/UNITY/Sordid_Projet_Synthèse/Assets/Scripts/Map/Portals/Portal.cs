using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    [SerializeField] float teleportDisableTimer = 1.5f;
    [SerializeField] float teleportLifetime = 5f;
    [SerializeField] float creationCD = 1;
    Collider2D siblingCollider;
    Collider2D teleportCollider;

    private void OnEnable()
    {
        teleportCollider.enabled = true;
        StartCoroutine(DisableIn());
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        float currentTime = 0;
        teleportCollider.enabled = false;
        while (currentTime < creationCD)
        {
            currentTime += Time.deltaTime;
            transform.localScale = new Vector3(currentTime, currentTime, 1);
            yield return null;
        }
        teleportCollider.enabled = true;
    }

    IEnumerator DisableIn()
    {
        yield return new WaitForSeconds(teleportLifetime);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        siblingCollider = teleportPosition.GetComponent<Collider2D>();
        teleportCollider = GetComponent<Collider2D>();
    }



    IEnumerator DisableCollider()
    {
        siblingCollider.enabled = teleportCollider.enabled = false;
        teleportCollider.enabled = false;
        yield return new WaitForSeconds(teleportDisableTimer);
        teleportCollider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.position = teleportPosition.position;
        StartCoroutine(DisableCollider());
    }
}
