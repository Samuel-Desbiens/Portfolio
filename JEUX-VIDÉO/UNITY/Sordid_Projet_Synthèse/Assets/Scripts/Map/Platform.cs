using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Collider2D col;
    [SerializeField] float disableTime = 0.6f;
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void Disable(Collider2D other)
    {
        StartCoroutine(DisableCoroutine(other));
    }

    IEnumerator DisableCoroutine(Collider2D other)
    {
        Physics2D.IgnoreCollision(col, other);
        yield return new WaitForSeconds(disableTime);
        Physics2D.IgnoreCollision(col, other, false);

    }
}
