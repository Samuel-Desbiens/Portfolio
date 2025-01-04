using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlashUpgradeRotation : MonoBehaviour
{
    public void SetRotation(Vector3 FiringPos,Vector3 Direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270 + (-(Mathf.Atan2(FiringPos.x - Direction.x, FiringPos.y - Direction.y))) * Mathf.Rad2Deg)); //Fait en sorte que le slash face away du joueur
    }
}
