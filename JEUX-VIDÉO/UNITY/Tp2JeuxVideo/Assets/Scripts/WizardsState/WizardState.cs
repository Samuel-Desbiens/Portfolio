using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardsBehaviour wizardsBehaviour;

    protected GameObject Target;
    protected GameObject BulletContainer;

    protected const int FleeThreshold = 25;
    protected const int NormalThreshold = 65;
    protected const int FullThreshold = 100;

    protected const float Range = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        wizardsBehaviour = GetComponent<WizardsBehaviour>();
        BulletContainer = GameObject.Find("Bullets");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ManageStateChange();
    public abstract void SetTarget(GameObject target);
}
