using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerBubbleShieldState : NecromancerState
{
    float bubbleFinalSize = 125;
    float timeToFinalSize = 5;
    bool bubblePoped = false;
    private void Start()
    {
        animator.SetTrigger("AttackOrb");
        animator.SetBool("Charging",true);

        ExpandBubble();
    }

    void ExpandBubble()
    {
        StartCoroutine(ExpandBubbleCoroutine());
    }

    IEnumerator ExpandBubbleCoroutine()
    {

        Transform bubble = manager.bubbleShield;
        bubble.gameObject.SetActive(true);

        bubble.transform.position = transform.position;
        Vector3 initialScale = bubble.localScale;
        Vector3 finalScale = new Vector3(bubbleFinalSize, bubbleFinalSize, bubbleFinalSize);
        float elapsedTime = 0f;

        while (elapsedTime < timeToFinalSize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeToFinalSize);
            bubble.localScale = Vector3.Lerp(initialScale, finalScale, t);
            yield return null;
        }

        bubble.localScale = initialScale;
        PopBubble(bubble);

    }

    void PopBubble(Transform bubble)
    {
        bubble.gameObject.SetActive(false);
        bubblePoped = true;
        animator.SetBool("Charging", false);


    }


    public override void Action()
    {
    }

    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.BubbleShield;
    }

    public override void ManageStateChange()
    {
        if (bubblePoped)
        {
            if(Random.Range(0,2) == 0)
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.LaserGrid);
            }
            else
            {
                manager.ChangeState(NecromancerManager.NecromancerStates.Stunned);
            }
        }
    }
}
