using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] protected int initialPoolSize;

    private Queue<GameObject> arrowPool;
    void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        arrowPool = new Queue<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform);
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    public GameObject GetArrow()
    {
        GameObject pooledArrow;
        pooledArrow = arrowPool.Dequeue();
        pooledArrow.SetActive(true);
        return pooledArrow;
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrowPool.Enqueue(arrow);
    }
}
