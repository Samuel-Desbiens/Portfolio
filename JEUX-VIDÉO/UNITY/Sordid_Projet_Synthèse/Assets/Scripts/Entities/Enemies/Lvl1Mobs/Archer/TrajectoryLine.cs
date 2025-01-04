using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrajectoryLine : MonoBehaviour
{
  [SerializeField] private int segmentCount = 2;
  private Vector2[] segments;

  private List<Vector2> segmentsList;
  private LineRenderer lineRenderer;

  private

  void Start()
  {
    segments = new Vector2[segmentCount];

    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.positionCount = segmentCount;
  }

  void Update()
  {

  }

  public void DisplayTrajectory(Color color, Vector2 playerPosition)
  {
    gameObject.SetActive(true);
    List<Vector2> segmentsList = new List<Vector2>();

    Vector2 currentPos = transform.position;

    lineRenderer.startColor = color;
    lineRenderer.endColor = color;

    bool collidedWithPlayer = false;

    for (int i = 0; i < segmentCount && !collidedWithPlayer; i++)
    {

      RaycastHit2D hit = Physics2D.Raycast(currentPos, (playerPosition - currentPos).normalized, Vector2.Distance(currentPos, playerPosition));
      if (hit.collider != null && hit.collider.CompareTag("Player"))
      {
        collidedWithPlayer = true;
        currentPos = hit.point;
      }

      segmentsList.Add(currentPos);
    }

    Vector3[] segmentsArray = new Vector3[segmentsList.Count];
    for (int i = 0; i < segmentsList.Count; i++)
    {
      segmentsArray[i] = segmentsList[i];
    }

    lineRenderer.positionCount = segmentsList.Count;
    lineRenderer.SetPositions(segmentsArray);
  }

  public int getSegmentsCount()
  {
    return segmentCount;
  }


  public void ClearTrajectory()
  {
    gameObject.SetActive(false);
  }
}