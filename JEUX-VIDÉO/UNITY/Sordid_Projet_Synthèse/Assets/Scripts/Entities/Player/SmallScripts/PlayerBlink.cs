using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [SerializeField] float teleportRange = 10;
    Timer timer;
    [SerializeField] float blinkCD = 2f;
    BlinkFx blinkFx;

    private void Start()
    {
        blinkFx = GetComponent<BlinkFx>();
        timer = new(blinkCD);
    }
    void Update()
    {
        timer.Update();
    }
    public void Blink()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (timer.CanDo())
        {

            Vector2 tpDir = ((Vector2)mousePos - (Vector2)transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, tpDir, teleportRange, LayerMask.GetMask("Ground"));


            if (hit.collider != null)
            {
                Vector2 target = new Vector2(hit.point.x - tpDir.x, hit.point.y - tpDir.y);
                SetBlinkFXValues(transform.position, target);
                transform.position = target;
            }
            else if (Vector2.Distance(transform.position, mousePos) > teleportRange)
            {
                Vector2 target = (Vector2)transform.position + tpDir * teleportRange;
                SetBlinkFXValues(transform.position, target);
                transform.position = target;
            }
            else
            {
                SetBlinkFXValues(transform.position, mousePos);
                transform.position = mousePos;
            }
            timer.Reset();
        }
    }

    private void SetBlinkFXValues(Vector2 currentPos, Vector2 targetPos)
    {
        blinkFx.SetBlinkFXValues(currentPos, targetPos);

    }


}
