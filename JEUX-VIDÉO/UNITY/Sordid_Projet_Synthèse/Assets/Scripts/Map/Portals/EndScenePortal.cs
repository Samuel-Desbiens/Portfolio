using Harmony;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScenePortal : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;
    [SerializeField] Vector3 nextSceneSpawnPosition;
    LoadingScreen loadingScreen;
    GameObject col;
    SoundManager soundManager;
    private PlayerInputActions inputs;
    private InputAction interact;
    private void Awake()
    {
        inputs = new PlayerInputActions();
        interact = inputs.Player.Interact;
    }
    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
    private void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingScreen = LoadingScreen.instance;
        soundManager = SoundManager.Instance;
    }

    void Update()
    {
        if (col && interact.triggered)
        {
            ManageAchievements();
            loadingScreen.LoadScene(nextSceneIndex);
            soundManager.PlayAudio(soundManager.teleportClip, transform.position);
        }
    }
    private void ManageAchievements()
    {
        AchievementManager am = FindFirstObjectByType<AchievementManager>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Lvl1":
                am.SetAchievement("lvl1");
                break;
            case "Lvl2":
                am.SetAchievement("lvl2");
                break;
            case "MainBoss1":
                am.SetAchievement("boss1");
                break;
            case "Boss2":
                am.SetAchievement("boss2");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) col = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col = null;
        }
    }
}
