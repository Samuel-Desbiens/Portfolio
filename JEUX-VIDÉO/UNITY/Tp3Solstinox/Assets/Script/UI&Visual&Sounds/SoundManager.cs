using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static private SoundManager Instance;

    AudioSource AudioSource;

    public AudioClip GhostHitFx;

    public AudioClip WallHitFx;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayGhostHit()
    {
        AudioSource.PlayOneShot(GhostHitFx);
    }

    public void PlayWallHit()
    {
        AudioSource.PlayOneShot(WallHitFx);
    }
}
