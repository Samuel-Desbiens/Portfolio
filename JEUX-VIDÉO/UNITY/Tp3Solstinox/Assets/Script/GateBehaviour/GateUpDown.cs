using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUpDown : MonoBehaviour
{
    Vector3 BasePosition;
    Vector3 UpPosition;

    const float UpPositionOffset = 3f;

    const float UpSpeed = 5f;
    const float DownSpeed = 5f;

    bool goingUp;

    float pauseTimer;
    const float pauseTimerDown = 2f;
    const float pauseTimerUp = 3f;

    bool playOncePerMovement;

    //Audio
    AudioSource audioSource;

    public AudioClip Up;
    public AudioClip Down;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        BasePosition = transform.position;
        UpPosition = new Vector3(BasePosition.x, BasePosition.y + UpPositionOffset, BasePosition.z);

        goingUp = true;

        pauseTimer = pauseTimerUp;

        playOncePerMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseTimer == 0)
        {
            if (goingUp)
            {
                if(!playOncePerMovement)
                {
                    audioSource.PlayOneShot(Up);
                    playOncePerMovement = true;
                }

                transform.position = Vector3.MoveTowards(transform.position, UpPosition, UpSpeed * Time.deltaTime);

                if (transform.position == UpPosition)
                {
                    goingUp = false;
                    pauseTimer = pauseTimerDown;
                    playOncePerMovement = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, BasePosition, DownSpeed * Time.deltaTime);

                if (transform.position == BasePosition)
                {
                    goingUp = true;
                    pauseTimer = pauseTimerUp;
                    audioSource.PlayOneShot(Down);
                    playOncePerMovement = false;
                }
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;

            if (pauseTimer < 0)
            {
                pauseTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           if(!goingUp)
            {
                collision.gameObject.GetComponent<PlayerController>().PlayerDeathStart();
            }
        }
    }
}
