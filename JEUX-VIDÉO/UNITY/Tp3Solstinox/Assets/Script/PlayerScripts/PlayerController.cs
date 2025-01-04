using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController playerInstance;

    //Character Objects
    CharacterController controller;

    Direction LastChangeSceneDirection;

    AudioSource AudioSource;

    //Character Variable
    private const float MovementSpeed = 5;
    private const float JumpForce = 2;

    private bool grounded;

    private Vector3 Directions;
    private Vector3 Velocity;

    //Weapon Related
    GameObject WeaponContainer;

    const float MaxWeaponTimer = 0.5f;
    float actualWeaponTimer;

    //Death Variable

    float MaxDeathTimer = 2.5f;
    float ActualDeathTimer;

    //Pause related
    GameObject PauseText;

    private void Awake()
    {
        LastChangeSceneDirection = Direction.Down;
        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        actualWeaponTimer = 0;
        ActualDeathTimer = 0;

        //Objects Linking
        controller = GetComponent<CharacterController>();

        WeaponContainer = GameObject.Find("Projectile");

        AudioSource = gameObject.GetComponent<AudioSource>();

        PauseText = GameObject.Find("PauseText");
        PauseText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ActualDeathTimer == 0)
        {
            grounded = controller.isGrounded;

            if (grounded && Velocity.y < 0)
            {
                Velocity.y = 0;
            }

            //Character Movement

            //Assure Only One Direction at a time
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
            {
                Directions = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
            else
            {
                Directions = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
            }

            if (Directions != Vector3.zero)
            {
                transform.forward = Directions;
            }

            Directions *= MovementSpeed;
            controller.Move(Directions * Time.deltaTime);

            //Jump
            if (Input.GetButton("Jump") && grounded)
            {
                Velocity.y += Mathf.Sqrt(JumpForce * -Physics.gravity.y);
            }

            Velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(Velocity * Time.deltaTime);

            //Fire
            if (actualWeaponTimer > 0)
            {
                actualWeaponTimer -= Time.deltaTime;
                if (actualWeaponTimer < 0)
                {
                    actualWeaponTimer = 0;
                }
            }
            if (Input.GetButton("Fire1") && actualWeaponTimer == 0)
            {
                for (int i = 0; i < WeaponContainer.transform.childCount; i++)
                {
                    if (!WeaponContainer.transform.GetChild(i).gameObject.activeSelf)
                    {
                        WeaponContainer.transform.GetChild(i).gameObject.SetActive(true);

                        WeaponContainer.transform.GetChild(i).gameObject.GetComponent<WeaponBehaviour>().Thrown(transform.forward);

                        WeaponContainer.transform.GetChild(i).position = transform.position + transform.up;

                        actualWeaponTimer = MaxWeaponTimer;
                        break;
                    }
                }
            }
        }
        else
        {
            ActualDeathTimer -= Time.deltaTime;
            if(ActualDeathTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                ActualDeathTimer = 0;
            }
        }
       
        if(Input.GetButtonDown("Submit"))
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;

                PauseText.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;

                PauseText.SetActive(false);
            }
            
        }
    }

    public Direction GetSceneDirection()
    {
        return LastChangeSceneDirection;
    }

    public void SetSceneDirection(Direction next)
    {
        LastChangeSceneDirection = next;
    }

    public void TeleportPlayer(Vector3 position)
    {
        transform.position = position;
    }

    public void PlayerDeathStart()
    {
        if(ActualDeathTimer == 0)
        {
            AudioSource.PlayOneShot(AudioSource.clip);

            ActualDeathTimer = MaxDeathTimer;
        }
        
    }
}


