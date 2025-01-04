using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviours : MonoBehaviour
{
    SpriteRenderer spriteR;

    int dmg;
    const int dmgMax = 3;
    const int dmgMin = 1;

    const float bulletSpeed = 0.01f;

    GameObject Shooter;
    GameObject Target;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();

        if (gameObject.name.Contains("Green"))
        {
            spriteR.sprite = Resources.Load<Sprite>("Sprites/ProjectileGreen");
        }
        else
        {
            spriteR.sprite = Resources.Load<Sprite>("Sprites/ProjectileBlue");
        }
    }
    private void OnEnable()
    {
        dmg = Random.Range(dmgMin, dmgMax);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, bulletSpeed);

        if(transform.position == Target.transform.position)
        {
            if(Target.CompareTag("Wizards"))
            {
                if(Target.GetComponent<WizardsBehaviour>().ModifyHealth(-dmg))
                {
                    if(Shooter.GetComponent<WizardsBehaviour>().GetState() == WizardsBehaviour.WizardPossibleStates.Normal)
                    {
                        Shooter.GetComponent<WizardStateNormal>().AddKill();
                    }
                }
            }
            else
            {
                Target.GetComponent<TowersBehaviour>().TakeDMG(-dmg);
            }
            gameObject.SetActive(false);
        }
    }


    public void SetStart(GameObject owner, GameObject target)
    {
        Shooter = owner;
        Target = target;
    }
}
