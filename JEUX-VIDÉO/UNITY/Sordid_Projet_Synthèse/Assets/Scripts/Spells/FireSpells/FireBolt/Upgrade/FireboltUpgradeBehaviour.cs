using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireboltUpgradeBehaviour : MonoBehaviour, SpellBehaviour
{
    private Rigidbody2D RB;

    private const float Damage = 8;
    private const float Range = 20f;
    private const float Cooldown = 1;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Fire;

    SoundManager soundManager;

    private const string Description = "The first fire spell of any wizards! Causes more than 100 house fire every month";

    private const float AngleModifier = 50;

    private PlayerInventory playerInventory;


    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        soundManager = SoundManager.Instance;

        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

    }



    private void OnEnable()
    {
        this.ResetSpell();
        this.Cast();
    }

    private void ResetSpell()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Child = transform.GetChild(i).gameObject;
            Child.transform.position = transform.position;
            Child.SetActive(false);
        }
    }

    private void Cast()
    {
        Vector3 MouseToCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = transform.position;
        
        float FirstAngle = Mathf.Atan2((MouseToCamPos - pos).y, (MouseToCamPos - pos).x);
        
        FirstAngle -= AngleModifier * 2;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Child = transform.GetChild(i).gameObject;
            Child.SetActive(true);
            Child.GetComponent<IndividualFireBoltUpgradeBehaviour>().LaunchAndDirection(new Vector2(Mathf.Cos(FirstAngle + (AngleModifier * i)), Mathf.Sin(FirstAngle + (AngleModifier * i))));

            soundManager.PlayAudio(soundManager.fireBoltClip, pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool CheckIfChildActive = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
            {
                CheckIfChildActive = true;
                break;
            }
        }

        if(!CheckIfChildActive)
        {
            gameObject.SetActive(false);
        }
    }

    public float GetDamage()
    {
        return Damage + (Damage / 100) * playerInventory.GetPowerBonus();
    }
    public float GetCooldown()
    {
        return Cooldown;
    }

    public SpellBehaviour.SpellElements GetElements()
    {
        return Element;
    }

    public string GetDescription()
    {
        return Description;
    }

    //Other Accessor

    public float GetRange()
    {
        return Range;
    }

}
