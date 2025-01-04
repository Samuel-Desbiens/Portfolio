using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDiscUpgradeBehaviour : MonoBehaviour,SpellBehaviour
{
    private float Damage = 8f;
    private float KnockBackForce = 10f;
    private float Range = 15f;
    private float MovementSpeed = 12f;
    private float Cooldown = 1.5f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Air;

    SoundManager soundManager;

    private const string Description = "A spinning disc of air cutting everything in its path";

    private float NBChilds;

    GameObject FirstAirSlash;
    SingularAirDiscUpgradeBehaviour FirstAirSlashScript;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        NBChilds = transform.childCount;
        FirstAirSlash = transform.GetChild(0).gameObject;
        FirstAirSlashScript = FirstAirSlash.GetComponent<SingularAirDiscUpgradeBehaviour>();
        soundManager = SoundManager.Instance;
    }

    private void OnEnable()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        soundManager.PlayAudio(soundManager.airBoomrangClip, transform.position);
        this.ResetSpell();
    }

    private void ResetSpell()
    {
       if(!FirstAirSlash.activeSelf)
        {
            ChangeState();
        }

       
    }

    void Update()
    {
        if(FirstAirSlashScript.GetDone() && FirstAirSlash.activeSelf)
        {
            ChangeState();
        }

        bool deactivate = true;

        for (int i = 0; i < NBChilds; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                deactivate = false;
                break;
            }
        }

        if (deactivate)
        {
             gameObject.SetActive(false);
        } 
    }

    private void ChangeState()
    {
        if (FirstAirSlash.activeSelf)
        {
            FirstAirSlash.SetActive(false);
            transform.position = FirstAirSlash.transform.position;
            for (int i = 1; i < NBChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            FirstAirSlash.SetActive(true);
            for (int i = 1; i < NBChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
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

    //Other Getter Necessaire vue comment le spell fonctionne

    public float GetRange()
    {
        return Range;
    }

    public float GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public float GetKnockBack()
    {
        return KnockBackForce;
    }
}
