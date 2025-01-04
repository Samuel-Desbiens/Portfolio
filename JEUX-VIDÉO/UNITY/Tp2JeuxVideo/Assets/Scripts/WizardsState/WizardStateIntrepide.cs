using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateIntrepide : WizardState
{
    int RegenTimer;
    const int RegenTimerDelay = 50;
    const int AmountRegened = 1;

    int AttackTimer;
    const int AttackTimerDelay = 25;

    const float NormalMovementSpeed = 0.00625f;
    float actualMovementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RegenTimer = 0;

        actualMovementSpeed = NormalMovementSpeed;

        Target = wizardsBehaviour.GetRandomEnemyTower();
    }

    // Update is called once per frame
    void Update()
    {
        if (wizardsBehaviour.GetBushSafe())
        {
            actualMovementSpeed = NormalMovementSpeed / 2;
        }
        else
        {
            actualMovementSpeed = NormalMovementSpeed;
        }

        if(!Target.activeSelf)
        {
            Target = wizardsBehaviour.GetRandomEnemyTower();
        }
        if (Vector2.Distance(transform.position, Target.transform.position) > Range)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, actualMovementSpeed);
        }
        else if (AttackTimer >= AttackTimerDelay)
        {
            if (wizardsBehaviour.GetTeam())
            {
                for (int i = 0; i < BulletContainer.transform.GetChild(0).childCount; i++)
                {
                    if (!BulletContainer.transform.GetChild(0).GetChild(i).gameObject.activeSelf)
                    {
                        var bul = BulletContainer.transform.GetChild(0).GetChild(i);
                        bul.gameObject.SetActive(true);
                        bul.position = gameObject.transform.position;
                        bul.GetComponent<BulletBehaviours>().SetStart(gameObject, Target);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BulletContainer.transform.GetChild(1).childCount; i++)
                {
                    if (!BulletContainer.transform.GetChild(1).GetChild(i).gameObject.activeSelf)
                    {
                        var bul = BulletContainer.transform.GetChild(1).GetChild(i);
                        bul.gameObject.SetActive(true);
                        bul.position = gameObject.transform.position;
                        bul.GetComponent<BulletBehaviours>().SetStart(gameObject, Target);
                        break;
                    }
                }
            }
            AttackTimer = 0;
        }


        if (RegenTimer >= RegenTimerDelay)
        {
            wizardsBehaviour.ModifyHealth(AmountRegened);
        }
        ManageStateChange();
    }

    private void FixedUpdate()
    {
        RegenTimer++;
        AttackTimer++;
    }

    public override void ManageStateChange()
    {
        if (wizardsBehaviour.GetHPPercent() <= FleeThreshold)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Fuite);
        }
    }

    public override void SetTarget(GameObject target)
    {
        Target = target;
    }
}
