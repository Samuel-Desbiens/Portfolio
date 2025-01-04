using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerManager : MonoBehaviour
{
    public enum NecromancerStates { BigOrb, LaserGrid, Summoning, Fleeing, Dead, Stunned, BubbleShield }
    private NecromancerState state;
    [SerializeField] private Health health;
    [SerializeField] ProjectilePool orbPool;
    public Transform firePoint;
    public List<LineRenderer> lasers;
	public List<LineRenderer> warningLasers;
    public List<Transform> possibleTp;
    public NecromancerStates lastState;
    public GameObject boss1;
    public bool boss1Summoned = false;
	public List<Enemy> possibleSummons;
    public Transform bubbleShield;
    public float fixedLaserYPos;
    SoundManager soundManager;
    //[SerializeField] float nextStageHealthPerc = 100;\//asd

    private bool dead;


    //[SerializeField] GameObject warningZone;
    public void OnDeath()
    {
        dead = true;
    }

    public NecromancerOrb GetOrb()
    {
        return orbPool.GetProjectile().GetComponent<NecromancerOrb>();
    }

    private void OnEnable()
    {
        if (dead)
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        health = GetComponent<Health>();
        soundManager = SoundManager.Instance;
        //warningZone.SetActive(false);
    }

    public float GetHPRatio()
    {
        return health.GetHPRatio();
    }


    // public float GetNextStageHealthPerc()
    // {
        // return nextStageHealthPerc;
    // }
    // public void ReduceNextStagePerc(float nextStagePercInc)
    // {
        // nextStageHealthPerc -= nextStagePercInc;
    // }

    void Awake()
    {
        state = GetComponent<NecromancerState>();
        fixedLaserYPos = transform.position.y;
    }

    public void ChangeState(NecromancerStates nextState)
    {
        lastState = state.GetStateType();
        Destroy(state);

        switch (nextState)
        {
            case NecromancerStates.BigOrb:
                {
                    state = gameObject.AddComponent<NecromancerBigOrbState>() as NecromancerBigOrbState;
                    soundManager.PlayAudio(soundManager.monster2Clip, transform.position);
                    break;
                }
            case NecromancerStates.LaserGrid:
                {
                    state = gameObject.AddComponent<NecromancerLaserGridState>() as NecromancerLaserGridState;
                    soundManager.PlayAudio(soundManager.monsterGrowlClip, transform.position);
                    break;
                }
            case NecromancerStates.Summoning:
                {   
                    state = gameObject.AddComponent<NecromancerSummoningState>() as NecromancerSummoningState;
                    soundManager.PlayAudio(soundManager.monsterGrowlClip, transform.position);
                    break;
                }
            case NecromancerStates.Fleeing:
                {
                    state = gameObject.AddComponent<NecromancerFleeingState>() as NecromancerFleeingState;
                    soundManager.PlayAudio(soundManager.monster3Clip, transform.position);
                    break;
                }
            case NecromancerStates.Dead:
                {
                    state = gameObject.AddComponent<NecromancerDeadState>() as NecromancerDeadState;
                    soundManager.PlayAudio(soundManager.monster3Clip, transform.position);
                    break;
                }
            case NecromancerStates.Stunned:
                {
                    state = gameObject.AddComponent<NecromancerStunnedState>() as NecromancerStunnedState;
                    soundManager.PlayAudio(soundManager.monster1Clip, transform.position);
                    break;
                }
            case NecromancerStates.BubbleShield:
                {
                    state = gameObject.AddComponent<NecromancerBubbleShieldState>() as NecromancerBubbleShieldState;
                    soundManager.PlayAudio(soundManager.monster3Clip, transform.position);
                    break;
                }
        }
    }
}
