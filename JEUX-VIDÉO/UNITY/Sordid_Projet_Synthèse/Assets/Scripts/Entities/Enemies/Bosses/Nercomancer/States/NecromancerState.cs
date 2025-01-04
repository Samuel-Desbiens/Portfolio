using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using static NecromancerManager;

public abstract class NecromancerState : MonoBehaviour
{
    protected NecromancerManager manager;
    protected SpriteRenderer spriteRen;
    protected GameObject player;
    protected Animator animator;

    //colliders
    protected CapsuleCollider2D capsuleTrigger;
    protected BoxCollider2D boxTrigger;
    protected PolygonCollider2D polygonCollider;

    private Side facing = Side.RIGHT;

    protected SoundManager soundManager;
    [SerializeField] protected float flipCooldown = 0.5f;
    bool canFlip = true;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        GetComponents();
        Debug.Log(GetStateType().ToString());
    }

    void Update()
    {
        FacePlayer();
        Action();
        ManageStateChange();
    }


    private void GetComponents()
    {
        manager = GetComponent<NecromancerManager>();
        spriteRen = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleTrigger = GetComponent<CapsuleCollider2D>();
        boxTrigger = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    public Side IsFacing()
    {
        return facing;
    }

    protected virtual void FacePlayer()
    {
        if (canFlip)
        {
            if (player.transform.position.x < transform.position.x && transform.localScale.x > 0)
            {
                StartCoroutine(FlipSprite(true));
            }
            else if (player.transform.position.x > transform.position.x && transform.localScale.x < 0)
            {
                StartCoroutine(FlipSprite(false));
            }
        }
    }

    private IEnumerator FlipSprite(bool flip)
    {
        canFlip = false;
        yield return new WaitForSeconds(flipCooldown);
        Vector3 newScale = transform.localScale;
        if (flip)
        {
            facing = Side.LEFT;
        }
        else
        {
            facing = Side.RIGHT;
        }
        newScale.x = Mathf.Abs(newScale.x) * (int)facing;
        transform.localScale = newScale;
        canFlip = true;
    }

    public abstract NecromancerStates GetStateType();
    public abstract void Action();
    public abstract void ManageStateChange();
}
