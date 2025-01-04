using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerFleeingState : NecromancerState
{
    [SerializeField] float speed = 7;
    [SerializeField] float minDistanceToLaser = 25;
    [SerializeField] float maxFleeTime = 15;
    bool hit = false;
    bool gotoBubbleShield = false;
    BlinkFx blinkFx;
    bool teleported = false;

    private void Start()
    {
        hit = false;
        blinkFx = GetComponent<BlinkFx>();
        animator.SetTrigger("Fleeing");
        Invoke(nameof(EscapeFlee), maxFleeTime);
    }

    void EscapeFlee()
    {
        gotoBubbleShield = true;
    }


    public override void Action()
    {
        Move();
        if (hit && !teleported)
        {
            Teleport();
        }
    }
    void Teleport()
    {
        Vector2 tpPos = manager.possibleTp.Random().transform.position;
        blinkFx.SetBlinkFXValues(transform.position, tpPos);
        transform.position = tpPos;
        teleported = true;
        hit = false;
    }
    void Move()
    {
        Vector2 dir = (transform.position - player.transform.position).normalized;
        dir.y += 0.2f;
        transform.Translate(speed * Time.deltaTime * dir);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Colision layer : " + collision.gameObject.layer + " Layer mask is : " + LayerMask.NameToLayer("Spell"));
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spell"))
        {
            hit = true;
        }
    }


    public override NecromancerManager.NecromancerStates GetStateType()
    {
        return NecromancerManager.NecromancerStates.Fleeing;
    }

    public override void ManageStateChange()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist > minDistanceToLaser)
        {
            manager.ChangeState(NecromancerManager.NecromancerStates.Summoning);
        }
        else if (teleported && hit || gotoBubbleShield)
        {
            manager.ChangeState(NecromancerManager.NecromancerStates.BubbleShield);
        }

    }
}
