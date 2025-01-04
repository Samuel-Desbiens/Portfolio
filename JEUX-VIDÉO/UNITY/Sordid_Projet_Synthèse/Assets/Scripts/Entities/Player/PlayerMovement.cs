using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IOnGameReset
{
    //Inputs
    private PlayerInputActions inputs;
    private InputAction move;
    private InputAction jump;
    private InputAction bootAbility;

    //Abilities
    Timer timer;
    float dashCooldown = 3f;

    //Inventory
    Inventory inventory;


    //Movement
    private const float BASE_SPEED = 12;
    float currentSpeed = BASE_SPEED;
    private const float knockBackForce = 50;

    //Jump
    private float sideJumpForce = 2.0f;
    private bool grounded = true;
    private bool doubleJumped = false;
    private bool tripleJumped = false;
    [SerializeField] float jumpForce = 12f;


    //Components
    Animator animator;
    Rigidbody2D rb;
    private Health healthBehavior;
    private LiquidBar healthBar;
    GameObject projectilesGO;
    SoundManager soundManager;

    SpriteRenderer sprite;
    PlayerBlink blink;

    private void Awake()
    {
        inputs = new PlayerInputActions();
        move = inputs.Player.Move;
        jump = inputs.Player.Jump;
        bootAbility = inputs.Player.BootAbility;
    }

    void Start()
    {
        DontDestroyManager.instance.AddItem(this);
        healthBehavior = gameObject.GetComponent<Health>();
        healthBar = Finder.FindWithTag<LiquidBar>("HealthBar");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        timer = new(dashCooldown);
        blink = GetComponent<PlayerBlink>();
        animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
        bootAbility.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        bootAbility.Disable();
    }

    void Update()
    {
        healthBar.currentFillAmount = healthBehavior.GetHPRatio();

        Movements();
    }

    public void IncreaseMaxHealth(float amount)
    {
        healthBehavior.IncreaseMaxHealth(amount);
    }

    public void DecreaseMaxHealth(float amount)
    {
        healthBehavior.DecreaseMaxHealth(amount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y) * knockBackForce);
        }
    }

    #region Movement
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("Platform"))
        {
            if (!grounded)
            {
                animator.SetTrigger("landing");
            }
            grounded = true;
            doubleJumped = false;
            tripleJumped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }

    void Movements()
    {

        float hInputs = move.ReadValue<Vector2>().x;
        animator.SetFloat("xSpeed", Mathf.Abs(hInputs));
        FlipCharacter(hInputs);
        Move(hInputs);
        Jump(hInputs);
        CheckAbility();
    }

    private void Move(float hInputs)
    {
        if (!grounded)
        {
            hInputs = hInputs / 1.5f;
        }
        transform.Translate(new Vector2(hInputs * currentSpeed * Time.deltaTime, 0));
    }
    private void Jump(float hInputs)
    {
        animator.SetBool("grounded", grounded);
        animator.SetFloat("ySpeed", GetComponent<Rigidbody2D>().velocityY);
        float sign = SideJump(hInputs);
        if (jump.triggered)
        {
            if (CheckJump())
            {
                if (grounded)
                {
                    animator.SetTrigger("jump");
                }
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(sideJumpForce * sign, jumpForce), ForceMode2D.Impulse);
                soundManager.PlayAudio(soundManager.jumpClip, transform.position);
            }
        }
    }

    private float SideJump(float hInputs)
    {
        if (hInputs > 0)
        {
            return 0.5f;
        }
        else if (hInputs < 0)
        {
            return -0.5f;
        }
        else
        {
            return 0;
        }
    }

    private bool CheckJump()
    {
        if (grounded)
        {
            return true;
        }
        else if (!doubleJumped)
        {
            doubleJumped = true;
            return true;
        }
        else if (!tripleJumped && GetBootsAbility() == BootsAbility.TripleJump)
        {
            tripleJumped = true;
            return true;
        }
        else { return false; }
    }

    private void FlipCharacter(float hInputs)
    {
        if (hInputs < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (hInputs > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void CheckAbility()
    {

        if (GetBootsAbility() == BootsAbility.Dash)
        {
            timer.Update();
        }
        if (bootAbility.triggered)
        {
            switch (GetBootsAbility())
            {
                case BootsAbility.Dash:
                    {
                        if (timer.CanDo())
                        {
                            rb.AddForce(new Vector2((sideJumpForce * Input.GetAxis("Horizontal")) * currentSpeed / 2, 0), ForceMode2D.Impulse);
                            timer.Reset();
                        }

                    }
                    break;

                case BootsAbility.Teleport:
                    {
                        blink.Blink();
                    }
                    break;

                case BootsAbility.TripleJump:
                    {
                        grounded = false;
                        Jump(Input.GetAxis("Horizontal"));
                    }
                    break;

                default:
                    {

                    }
                    break;
            }
        }
    }


    #endregion

    public void SetSpeedBoost()
    {
        if (inventory != null)
        {
            currentSpeed = inventory.GetSpeedBoost() + BASE_SPEED;
        }
    }
    private BootsAbility GetBootsAbility()
    {
        return inventory.GetCurrentBootAbility();
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void ResetGO()
    {
        healthBehavior.ResetGO();
    }


    public void ChangePlayerState(bool state)
    {
        rb.simulated = sprite.enabled = state;
    }
}
