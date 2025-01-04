using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    int NbEnemyKilled;

    bool Attacking;

    int AttackTimer;
    const int AttackTimerDelay = 25;

    int RegenTimer;
    const int RegenTimerDelay = 50;
    const int AmountRegened = 1;

    //J'ai l'impression qui aurait une meilleure manière de faire qu'un nombre si petit mais bon...
    const float NormalMovementSpeed = 0.005f;
    float actualMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RegenTimer = 0;

        NbEnemyKilled = 0;
        Attacking = false;
        actualMovementSpeed = NormalMovementSpeed;
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
        if (!Attacking)
        {
            for(int i = 0; i< wizardsBehaviour.GetVision().Count;i++)
            {
                if(wizardsBehaviour.GetVision()[i].CompareTag("Wizards"))
                {
                    if (wizardsBehaviour.GetVision()[i].GetComponent<WizardsBehaviour>().GetTeam() != wizardsBehaviour.GetTeam())
                    {
                        Target = wizardsBehaviour.GetVision()[i];
                        break;
                    }
                }
                else if(wizardsBehaviour.GetVision()[i].CompareTag("Tower"))
                {
                    if (wizardsBehaviour.GetVision()[i].GetComponent<TowersBehaviour>().GetTeam() != wizardsBehaviour.GetTeam())
                    {
                        Target = wizardsBehaviour.GetVision()[i];
                        break;
                    }
                }
                
            }
            if(Target != null)
            {
                if (!Target.activeSelf)
                {
                    Target = wizardsBehaviour.GetRandomEnemyTower();
                }
                transform.up = Target.transform.position - transform.position;
                if (Vector2.Distance(transform.position, Target.transform.position) <= Range)
                {
                    Attacking = true;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, actualMovementSpeed);
                }
            }
            else
            {
                Target = wizardsBehaviour.GetRandomEnemyTower();
            }
            if(RegenTimer >= RegenTimerDelay)
            {
                wizardsBehaviour.ModifyHealth(AmountRegened);
                RegenTimer = 0;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, Target.transform.position) > Range || !Target.gameObject.activeSelf)
            {
                Attacking = false;
            }
            else if (AttackTimer >= AttackTimerDelay)
            {
                if(wizardsBehaviour.GetTeam())
                {
                    for(int i = 0;i<BulletContainer.transform.GetChild(0).childCount;i++)
                    {
                        if(!BulletContainer.transform.GetChild(0).GetChild(i).gameObject.activeSelf)
                        {
                            var bul = BulletContainer.transform.GetChild(0).GetChild(i);
                            bul.gameObject.SetActive(true);
                            bul.position = gameObject.transform.position;
                            bul.GetComponent<BulletBehaviours>().SetStart(gameObject,Target);
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
                            bul.GetComponent<BulletBehaviours>().SetStart(gameObject,Target);
                            break;
                        }
                    }
                }
                AttackTimer = 0;
            }
        }
        ManageStateChange();
    }

    private void FixedUpdate()
    {
        AttackTimer++;
        if(!Attacking)
        {
            RegenTimer++;
        }
    }

    public override void ManageStateChange()
    { 
        if(wizardsBehaviour.GetHPPercent() <= FleeThreshold)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Fuite);
        }
        else if(NbEnemyKilled >= 3)
        {
            wizardsBehaviour.ChangeState(WizardsBehaviour.WizardPossibleStates.Intrepide);
        }
    }

    public override void SetTarget(GameObject target)
    {
        Target = target;
    }

    public void AddKill()
    {
        NbEnemyKilled++;
    }

}
