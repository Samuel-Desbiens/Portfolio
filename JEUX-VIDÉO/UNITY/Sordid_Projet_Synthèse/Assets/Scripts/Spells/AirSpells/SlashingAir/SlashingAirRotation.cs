using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingAirRotation : MonoBehaviour
{

    public void SetRotation(Vector3 FiringPos)
    {
        Vector3 MousPosToCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0,270 +(-(Mathf.Atan2(FiringPos.x - MousPosToCam.x, FiringPos.y - MousPosToCam.y))) * Mathf.Rad2Deg)); //Fait en sorte que le slash face away du joueur
    }
}
