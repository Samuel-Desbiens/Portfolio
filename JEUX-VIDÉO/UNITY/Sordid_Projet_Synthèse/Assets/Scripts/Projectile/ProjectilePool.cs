using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] GameObject projectileAsset;
    public GameObject GetProjectile()
    {
        foreach(Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return Instantiate(projectileAsset, transform);
    }
}
