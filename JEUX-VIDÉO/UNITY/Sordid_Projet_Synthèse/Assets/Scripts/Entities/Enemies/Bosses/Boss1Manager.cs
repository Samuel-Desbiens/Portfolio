using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss1Manager : MonoBehaviour
{
  public enum Boss1States { Shooting, Up, Frenzy, Dead }
  private Boss1State state;
  [SerializeField] private Health health;
  [SerializeField] float nextStageHealthPerc = 100;
  [SerializeField] GameObject lights;
  private bool dead;
  private bool doorsClosed = false;
  [SerializeField] float closeDoorDistance = 30;
  private GameObject player;
  SoundManager soundManager;
  private float groundYPos;

  bool wentUp = false;

  [SerializeField] UnityEvent closeDoors;
  [SerializeField] UnityEvent openDoors;


  [SerializeField] GameObject warningZone;
  public void OnDeath()
  {
    openDoors.Invoke();
    dead = true;
    ChangeState(Boss1States.Dead);
  }
  public bool IsDoorsClosed()
  {
    return doorsClosed;
  }

  void Awake()
  {
    state = GetComponent<Boss1State>();
    soundManager = SoundManager.Instance;
  }
  private void Update()
  {
    if (Vector2.Distance(transform.position, player.transform.position) <= closeDoorDistance)
    {
      doorsClosed = true;
      closeDoors.Invoke();
    }
  }

  private void Start()
  {
    player = GameObject.FindWithTag("Player");
    health = GetComponent<Health>();
    warningZone.SetActive(false);
    groundYPos = Physics2D.Raycast(transform.position, Vector2.down, 200, LayerMask.GetMask("Ground")).point.y;
  }

  public void SetLightsOn(bool on)
  {
    lights.SetActive(on);
  }

  public float GetGroundLvl()
  {
    return groundYPos;
  }
  public float GetHPRatio()
  {
    return health.GetHPRatio();
  }

  public bool IsWentUp()
  {
    return wentUp;
  }

  public void WentUp()
  {
    wentUp = true;
  }

  public float GetNextStageHealthPerc()
  {
    return nextStageHealthPerc;
  }
  public void ReduceNextStagePerc(float nextStagePercInc)
  {
    nextStageHealthPerc -= nextStagePercInc;
  }
  public void ChangeState(Boss1States nextState)
  {
    Destroy(state);

    switch (nextState)
    {
      case Boss1States.Shooting:
        {
          state = gameObject.AddComponent<Boss1ShootingState>();
          soundManager.PlayAudio(soundManager.monsterGrowlClip, transform.position);
          break;
        }
      case Boss1States.Frenzy:
        {
          state = gameObject.AddComponent<Boss1FrenzyState>();
          soundManager.PlayAudio(soundManager.monsterGrowlClip, transform.position);
          break;
        }
      case Boss1States.Up:
        {
          state = gameObject.AddComponent<Boss1UpState>();
          soundManager.PlayAudio(soundManager.monsterGrowlClip, transform.position);
          break;
        }
      case Boss1States.Dead:
        {
          state = gameObject.AddComponent<Boss1DeadState>();
          soundManager.PlayAudio(soundManager.monster3Clip, transform.position);
          break;
        }
    }
  }
}
