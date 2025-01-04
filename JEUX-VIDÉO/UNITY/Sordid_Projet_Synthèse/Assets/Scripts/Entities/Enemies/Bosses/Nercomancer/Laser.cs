using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    [SerializeField] float edgeRadius =0.3f;
    [SerializeField] float dmgPerSec = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.edgeRadius = edgeRadius;
        gameObject.SetActive(false);
    }


    public void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log(Time.deltaTime * dmgPerSec);
            collision.gameObject.GetComponent<Health>().TakeDmg(Health.invincibilityTime * dmgPerSec);

        }
    }
}
