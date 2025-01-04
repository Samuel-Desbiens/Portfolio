using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWaveBehaviour : MonoBehaviour, SpellBehaviour
{
    private float Damage = 20;
    private float Cooldown = 4;
    private float SeedMovementSpeed = -5;
    private float WaveMovementSpeed = 4;
    private float Knockback = 0;
    private SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Water;

    private const string Description = "An icy wave freezing anything in its path";

    private Transform Seed;
    private Transform IceWave;
    private Transform MiniIceWave;

    private Vector3 BaseLocalPositionSeed;
    private Vector3 BaseLocalPositionIceWave;
    private Vector3 BaseLocalPositionMiniIceWave;

    private float Direction;

    GameObject Player;

    private void Awake()
    {
        Seed = transform.GetChild(0);

        IceWave = transform.GetChild(1);
        BaseLocalPositionIceWave = IceWave.localPosition;

        if (transform.childCount > 2)
        {
            MiniIceWave = transform.GetChild(2);
            BaseLocalPositionMiniIceWave = MiniIceWave.localPosition;
        }

        Player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        this.ResetSpell();
        this.Cast();   
    }

    private void ResetSpell()
    {
        Direction = 1;

        if (!Seed.gameObject.activeSelf)
        {
            ChangeState(false);
        }
        Seed.localPosition = BaseLocalPositionSeed;
        IceWave.localPosition = BaseLocalPositionIceWave;
        if (MiniIceWave != null)
        {
            MiniIceWave.localPosition = BaseLocalPositionMiniIceWave;
        }
    }
    private void Cast()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Player.transform.position.x)
        {
            Direction = -Direction;
        }           
    }

    // Update is called once per frame
    void Update()
    {
        if(Seed.gameObject.activeSelf)
        {
            transform.Translate(new Vector3(0, SeedMovementSpeed, 0) * Time.deltaTime);
        }
        else
        {
            if(MiniIceWave != null)
            {
                if (!IceWave.gameObject.activeSelf && !MiniIceWave.gameObject.activeSelf)
                {
                    gameObject.SetActive(false);
                }
            }
            else if(!IceWave.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ChangeState(bool state)
    {
        //if(Seed.gameObject.activeSelf)
        //{
            Seed.gameObject.SetActive(!state);
            IceWave.gameObject.SetActive(state);
            if (MiniIceWave != null)
            {
                MiniIceWave.gameObject.SetActive(state);
            }
        //}
        //else
        //{
        //    Seed.gameObject.SetActive(true);
        //    IceWave.gameObject.SetActive(false);
        //    if (MiniIceWave != null)
        //    {
        //        MiniIceWave.gameObject.SetActive(false);
        //    }

        //}
    }

    public float GetCooldown()
    {
        return Cooldown;
    }

    public float GetDamage()
    {
        return Damage + (Damage / 100) * Player.GetComponent<PlayerInventory>().GetPowerBonus();
    }

    public SpellBehaviour.SpellElements GetElements()
    {
        return Element;
    }

    public float GetKnockback()
    {
        return Knockback;
    }

    public float GetDirection()
    {
        return this.Direction;
    }

    public float GetMovementSpeed()
    {
        return WaveMovementSpeed;
    }

    public string GetDescription()
    {
        return Description;
    }
}
