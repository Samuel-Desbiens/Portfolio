using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateCacher : WizardState
{

    int AttackTimer;
    const int AttackTimerDelay = 25;

    bool Regened;

    int RegenTimer;
    const int RegenTimerDelay = 25;
    const int AmountRegened = 1;
    // Start is called before the first frame update
    void Start()
    {
        RegenTimer = 0;
        Regened = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < wizardsBehaviour.GetVision().Count; i++)
        {
            if (wizardsBehaviour.GetVision()[i].CompareTag("Wizards"))
            {
                if (wizardsBehaviour.GetVision()[i].GetComponent<WizardsBehaviour>().GetTeam() != wizardsBehaviour.GetTeam())
                {
                    Target = wizardsBehaviour.GetVision()[i];
                    break;
                }
            }
        }
        if(Target != null)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) <= Range && Target.activeSelf && AttackTimer >= AttackTimerDelay)
            {
                transform.up = Target.transform.position - transform.position;
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
            else
            {
                Target = null;
            }
        }
        else
        {
            if (RegenTimer >= RegenTimerDelay)
            {
                wizardsBehaviour.ModifyHealth(AmountRegened);
                if (wizardsBehaviour.GetHPPercent() >= FleeThreshold)
                {
                    Regened = true;
                }
            }
        }
        
        ManageStateChange();
    }

    private void FixedUpdate()
    {
        AttackTimer++;
        RegenTimer++;
    }

    public override void ManageStateChange()
    {
        if (wizardsBehaviour.GetHPPercent() <= FleeThreshold && Regened || (!wizardsBehaviour.GetBushSafe()))
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Fuite);
        }
        else if ((wizardsBehaviour.GetHPPercent() >= FullThreshold) || (wizardsBehaviour.GetHPPercent() >= NormalThreshold && wizardsBehaviour.GetVisionNBEnemy() >0))
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Normal);
        }
    }

    public override void SetTarget(GameObject target)
    {
        Target = target;
    }
}
