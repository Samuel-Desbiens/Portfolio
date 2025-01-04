using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    [SerializeField] private float restPosition = 0f;
    [SerializeField] private float pressedPosition = 45f;
    [SerializeField] private float hitStrenght = 100000f;
    [SerializeField] private float flipperDamper = 5;

    [SerializeField] private string inputName;

    private HingeJoint flipperHingeJoint;
    private JointSpring jointSpring = new JointSpring();

    void Start()
    {
        flipperHingeJoint = GetComponent<HingeJoint>();
        flipperHingeJoint.useSpring = true;
    }

    void Update()
    {
        MoveFlipper();
    }
    private void MoveFlipper()
    {
        jointSpring.spring = hitStrenght;
        jointSpring.damper = flipperDamper;
        if (gameObject.tag == "Right")
        {
            if (Input.GetButton("RightFlippers"))
            {
                jointSpring.targetPosition = pressedPosition;
            }
            else
            {
                jointSpring.targetPosition = restPosition;
            }
        }
        else
        {
            if (Input.GetButton("LeftFlippers"))
            {
                jointSpring.targetPosition = pressedPosition;
            }
            else
            {
                jointSpring.targetPosition = restPosition;
            }
        }
        

        flipperHingeJoint.spring = jointSpring;
        flipperHingeJoint.useLimits = true;
    }
}