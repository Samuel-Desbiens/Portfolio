using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFuite : WizardState
{
    private int MinimumFuite;
    private const int MinimumFuiteStart = 50;

    const float NormalMovementSpeed = 0.0075f;
    float actualMovementSpeed;

    int RegenTimer;
    const int RegenTimerDelay = 50;
    const int AmountRegened = 1;
    // Start is called before the first frame update
    void Start()
    {
        MinimumFuite = MinimumFuiteStart;

        actualMovementSpeed = NormalMovementSpeed;

        RegenTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(wizardsBehaviour.GetBushSafe())
        {
            actualMovementSpeed = NormalMovementSpeed/2;
        }
        else
        {
            actualMovementSpeed = NormalMovementSpeed;
        }
        if (MinimumFuite <= 0)
        {
            for (int i = 0; i < wizardsBehaviour.GetVision().Count; i++)
            {
                if (wizardsBehaviour.GetVision()[i].CompareTag("Bush") || wizardsBehaviour.GetVision()[i].CompareTag("Tower"))
                {
                        Target = wizardsBehaviour.GetVision()[i];
                }
            }
            if (Target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, actualMovementSpeed);
                transform.up = Target.transform.position - transform.position;
            }
            else
            {
                if (wizardsBehaviour.GetTeam())
                {
                    transform.position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(1, 0, 0), actualMovementSpeed);
                    transform.up = (transform.position - new Vector3(1, 0, 0)) - transform.position;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), actualMovementSpeed);
                    transform.up = (transform.position + new Vector3(1, 0, 0)) - transform.position;
                }
            }
        }
        else
        {
            if(wizardsBehaviour.GetTeam())
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(1,0,0), actualMovementSpeed);
                transform.up = (transform.position - new Vector3(1, 0, 0)) - transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), actualMovementSpeed);
                transform.up =  (transform.position + new Vector3(1, 0, 0)) - transform.position;
            }
        }
        if(RegenTimer >= RegenTimerDelay)
        {
            wizardsBehaviour.ModifyHealth(AmountRegened);
            RegenTimer = 0;
        }
        ManageStateChange();
    }

    private void FixedUpdate()
    {
        RegenTimer++;
        if(MinimumFuite > 0)
        {
            MinimumFuite--;
        }
    }

    public override void ManageStateChange()
    {
       if(wizardsBehaviour.GetBushSafe() && MinimumFuite <= 0)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Cacher);
        }
       else if(wizardsBehaviour.GetTowerSafe() && MinimumFuite <=0)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Garrison);
        }
       else if (wizardsBehaviour.GetHPPercent() >= FullThreshold)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Normal);
        }
    }
    public override void SetTarget(GameObject target)
    {
        Target = target;
    }
}
