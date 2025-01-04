using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardsBehaviour : MonoBehaviour
{
    GameManager gameManager;

    int HitPoint;
    int StartingHitPoint;
    const int HitPointMax = 55;
    const int HitPointMin = 45;

    int Range;
    const int BasicRange = 5;

    SpriteRenderer spriteR;

    bool TowerSafe;
    bool BushSafe;

    bool Team;//True = Green/False = Blue

    List<GameObject> vision;

    public enum WizardPossibleStates {Normal,Intrepide,Fuite,Cacher,Garrison,off }

    WizardPossibleStates actualState;
    WizardState state;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteR = GetComponent<SpriteRenderer>();
        state = GetComponent<WizardState>();
        if(gameObject.transform.parent.gameObject.name.Contains("Green"))
        {
            Team = true;
            spriteR.sprite = Resources.Load<Sprite>("Sprites/WizardGreen");
        }
        else
        {
            Team = false;
            spriteR.sprite = Resources.Load<Sprite>("Sprites/WizardBlue");
        }

        TowerSafe = false;
        BushSafe = false;

        vision = new List<GameObject>();
    }

    private void OnEnable()
    {
        HitPoint = Random.Range(HitPointMin, HitPointMax);
        StartingHitPoint = HitPoint;
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] Vision = Physics2D.CircleCastAll(gameObject.transform.position, 2f, Vector2.zero, 0f);

        vision.Clear();
        foreach (RaycastHit2D seen in Vision)
        {
            if (seen.transform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                if (seen.transform.CompareTag("Tower") )
                {
                    vision.Add(seen.transform.gameObject);   
                }
                else if(!seen.transform.CompareTag("Untagged"))
                {
                    vision.Add(seen.transform.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            if(collision.GetComponent<TowersBehaviour>().GetTeam() == Team)
            {
                TowerSafe = true;
            }       
        }
        else if(collision.CompareTag("Bush"))
        {
            BushSafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            if (collision.GetComponent<TowersBehaviour>().GetTeam() == Team)
            {
                TowerSafe = false ;
            }
        }
        else if (collision.CompareTag("Bush"))
        {
            BushSafe = false;
        }
    }

    public bool GetTeam()
    {
        return Team;
    }

    public GameObject GetRandomEnemyTower()
    {
        return gameManager.GetRandomEnemyTower(Team);
    }

    public List<GameObject> GetVision()
    {
        return vision;
    }

    public int GetVisionNBEnemy()
    {
        int enemies = 0;
        for(int i = 0;i<vision.Count;i++)
        {
            if(vision[i].CompareTag("Wizards"))
            {
                if (vision[i].GetComponent<WizardsBehaviour>().GetTeam() != Team)
                {
                    enemies++;
                }
            }
            else
            {
                vision.Remove(vision[i]);
            }
        }
        return enemies;
    }

    public int GetHPPercent()
    {
        float Percent = ((float)HitPoint/(float)StartingHitPoint) * 100;
        return (int)Percent;
    }

    public bool ModifyHealth(int modifier)
    {
        if(modifier < 0)
        {
            if(BushSafe)
            {
                modifier += 1;
            }
            else if(actualState == WizardPossibleStates.Garrison)
            {
                modifier = 0;
            }
        }
        HitPoint += modifier;

        if(HitPoint> StartingHitPoint)
        {
            HitPoint = StartingHitPoint;
        }
        else if(HitPoint<= 0)
        {
            ChangeState(WizardPossibleStates.Normal);
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    public bool GetBushSafe()
    {
        return BushSafe;
    }

    public bool GetTowerSafe()
    {
        return TowerSafe;
    }

    public void ChangeState(WizardPossibleStates next)
    {
        Destroy(state);

        if (gameManager.GetDebugState())
        {
            Debug.Log("Changement de " + actualState + " Vers " + next);
        }

        actualState = next;

        if(next == WizardPossibleStates.Normal)
        {
            state = gameObject.AddComponent<WizardStateNormal>() as WizardStateNormal;
        }
        else if(next == WizardPossibleStates.Intrepide)
        {
            state = gameObject.AddComponent<WizardStateIntrepide>() as WizardStateIntrepide;
        }
        else if(next == WizardPossibleStates.Fuite)
        {
            state = gameObject.AddComponent<WizardStateFuite>() as WizardStateFuite;
        }
        else if(next == WizardPossibleStates.Cacher)
        {
            state = gameObject.AddComponent<WizardStateCacher>() as WizardStateCacher;
        }
        else if(next == WizardPossibleStates.Garrison)
        {
            state = gameObject.AddComponent<WizardStateGarrison>() as WizardStateGarrison;
        }
        else
        {
            state = gameObject.AddComponent<WizardState>() as WizardState;
        }
    }

    public WizardPossibleStates GetState()
    {
        return actualState;
    }

    public void Deactivate()
    {
        Destroy(state);
    }
}
