using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateGarrison : WizardState
{
    int RegenTimer;
    const int RegenTimerDelay = 15;
    const int AmountRegened = 1;
    // Start is called before the first frame update
    void Start()
    {
        RegenTimer = 0;

        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(RegenTimer>= RegenTimerDelay)
        {
            wizardsBehaviour.ModifyHealth(AmountRegened);
        }
        ManageStateChange();
    }

    private void FixedUpdate()
    {
        RegenTimer++;
    }

    public override void ManageStateChange()
    {
        if (wizardsBehaviour.GetHPPercent() >= FullThreshold || !wizardsBehaviour.GetTowerSafe())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Normal);
        }
    }

    public override void SetTarget(GameObject target)
    {
        Target = target;
    }
}
