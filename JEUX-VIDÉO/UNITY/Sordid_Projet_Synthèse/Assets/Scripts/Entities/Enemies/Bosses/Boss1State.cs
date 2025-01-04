using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public abstract class Boss1State : MonoBehaviour
{
    protected Boss1Manager manager;
    protected SpriteRenderer spriteRen;
    protected GameObject player;
    protected Animator animator;

    protected Vector2 startPos = new(86.5f, -56.59f);
    protected SoundManager soundManager;
    protected GameObject[] attackZones;

    //colliders
    protected CapsuleCollider2D capsuleTrigger;
    protected BoxCollider2D boxTrigger;
    protected PolygonCollider2D polygonCollider;

    [SerializeField] protected Side facing = Side.RIGHT;


    [SerializeField] protected float flipCooldown = 0.5f;
    bool canFlip = true;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        soundManager = SoundManager.Instance;
        attackZones = GameObject.Find("RoofProjZones").Children();
        GetComponents();
    }

    void Update()
    {
        FacePlayer();
        Action();
        ManageStateChange();
    }


    private void GetComponents()
    {
        manager = GetComponent<Boss1Manager>();
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
                StartCoroutine(FlipSpriteX(true));
            }
            else if (player.transform.position.x > transform.position.x && transform.localScale.x < 0)
            {
                StartCoroutine(FlipSpriteX(false));
            }
        }
    }

    private IEnumerator FlipSpriteX(bool flip)
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
    public abstract void Action();
    public abstract void ManageStateChange();
}
