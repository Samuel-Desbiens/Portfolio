using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFx : MonoBehaviour
{
    [SerializeField] float blinkLingerTime = 1.5f;
    [SerializeField] LineRenderer blinkFX;

    
    public void SetBlinkFXValues(Vector2 currentPos, Vector2 targetPos)
    {
        blinkFX.gameObject.SetActive(true);
        blinkFX.SetPosition(0, currentPos);
        blinkFX.SetPosition(1, new Vector2(currentPos.x + targetPos.x, currentPos.y + targetPos.y) / 2);
        blinkFX.SetPosition(2, targetPos);
        blinkFX.transform.GetChild(0).transform.position = currentPos;
        blinkFX.transform.GetChild(1).transform.position = targetPos;
        StartCoroutine(DisableBlink());

    }

    IEnumerator DisableBlink()
    {
        yield return new WaitForSeconds(blinkLingerTime);
        blinkFX.gameObject.SetActive(false);
    }
}
