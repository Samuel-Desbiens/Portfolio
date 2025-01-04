using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject linkedPortalPrefab;

    public GameObject GetAvailableLinkedPortal()
    {
        foreach(Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return Instantiate(linkedPortalPrefab, transform);
    }

}
