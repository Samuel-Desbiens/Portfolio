using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] portalPositions;
    [SerializeField] private float changeInterval;
    private float timer = 0f;

    void Start()
    {
        if (portalPositions.Length == 0)
        {
            enabled = false;
        }

        ChangePortalPosition();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            ChangePortalPosition();
        }
    }

    void ChangePortalPosition()
    {
        int randomIndex = Random.Range(0, portalPositions.Length);
        transform.position = portalPositions[randomIndex];
    }
}
