using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallRotationAndCollision : MonoBehaviour
{
  private const float maxImunity = 0.07f;
  private float immunityTimerLeft;

  private FireBallBehaviour parentScript;
  private CapsuleCollider2D FBcollider;
  [SerializeField] Animator animator;

  private void Awake()
  {
    parentScript = transform.parent.GetComponent<FireBallBehaviour>();
    FBcollider = GetComponent<CapsuleCollider2D>();
  }

  private void OnEnable()
  {
    immunityTimerLeft = maxImunity;
    FBcollider.enabled = false;
    animator.SetTrigger("start");
  }
  public void SetRotation(Vector3 FiringPos)
  {
        Vector3 MouseToCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270 + (-(Mathf.Atan2(FiringPos.x - MouseToCamPos.x, FiringPos.y - MouseToCamPos.y))) * Mathf.Rad2Deg)); //Fait en sorte que le fireball face away du firing Position;
  }

  private void Update()
  {
    if (immunityTimerLeft > 0)
    {
      immunityTimerLeft -= Time.deltaTime;
      if (immunityTimerLeft <= 0)
      {
        immunityTimerLeft = 0;
        FBcollider.enabled = true; ;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    //À Besoin d'être ici pour faire tourner la hitbox avec le visuel ...
    if (!collider.CompareTag("Player"))
    {
      parentScript.Explode();
    }
  }
}
