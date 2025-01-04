using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlashUpgradeBehaviour : MonoBehaviour,SpellBehaviour
{
    private float Damage = 5;
    private float Range = 12;
    private float Cooldown = 0.4f;
    private const SpellBehaviour.SpellElements Element = SpellBehaviour.SpellElements.Air;

    private const string Description = "A gust of wind with medium range";

    private const float KnockBackForce = 25;

    private float MovementSpeed = 15f;

    private float BaseAngle;
    private const float AngleModifier = 270; //Aucune idée pourquoi c'est le bon chiffre ... but it is!

    private Vector3 FiringPos;
    SoundManager soundManager;
    private int NBChild;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        NBChild = transform.childCount;
        soundManager = SoundManager.Instance;
    }

    private void OnEnable()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        this.Cast();
    }



    private void Cast()
    {
        FiringPos = transform.position;

        Vector3 MouseToCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        BaseAngle = (Mathf.Atan2((MouseToCamPos - FiringPos).y, (MouseToCamPos - FiringPos).x) - AngleModifier);

        for(int i = 0; i< NBChild; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<SingularAirSlashUpgradeBehaviour>().SetAngle(BaseAngle + (i * AngleModifier));

        }
        soundManager.PlayAudio(soundManager.airSoundClip, transform.position);
    }
    void Update()
    {
        bool deactivate = true ;

        for (int i = 0; i < NBChild; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
            {
                deactivate = false;
                break;
            }
        }

        if(deactivate)
        {
            gameObject.SetActive(false);
        }
    }

    public float GetCooldown()
    {
        return this.Cooldown;
    }

    public float GetDamage()
    {
        return Damage + (Damage / 100) * playerInventory.GetPowerBonus(); ;
    }

    public SpellBehaviour.SpellElements GetElements()
    {
        return Element;
    }

    public string GetDescription()
    {
        return Description;
    }

    //Other Accessor Needed for child here

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
