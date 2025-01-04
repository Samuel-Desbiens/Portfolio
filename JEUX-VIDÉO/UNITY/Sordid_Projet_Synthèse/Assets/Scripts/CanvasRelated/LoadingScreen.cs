using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    [SerializeField] Image loadingBarFill;
    [SerializeField] GameObject loadingBarBackground;
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float animationTime = 1;
    [SerializeField] int deadSceneId = 7;
    int lobbyIndex = 1;
    bool onSceneFinishedLoading = false;
    // Start is called before the first frame update

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lobbyIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(lobbyIndex);
    }
    public void LoadWin()
    {
        LoadScene(deadSceneId + 1);
        StartCoroutine(WaitForSceneLoaded(DeletePlayerAndInventory));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadLobby()
    {
        if(SceneManager.GetActiveScene().buildIndex != lobbyIndex)
        {
            Debug.Log($"active scene index : {SceneManager.GetActiveScene().buildIndex}, lobby index {lobbyIndex}");
            InventoryPersistence.instance.gameObject.SetActive(true);
            DontDestroyManager.instance.RemoveAllItems();
            Time.timeScale = 1;
            LoadScene(lobbyIndex);
            StartCoroutine(WaitForSceneLoaded(LoadPermenantChanges));
        }
    }
    
    void LoadPermenantChanges()
    {
        PermanentUpgradesManager.instance.UnloadAllUsedShops();
        PermanentUpgradesManager.instance.LoadAllUnlockedSouls();
        PlayerPersistence.instance.GetComponent<PlayerMovement>().ChangePlayerState(true);

    }

    public void LoadDeath()
    {
        LoadScene(deadSceneId);
        StartCoroutine(WaitForSceneLoaded(DeletePlayerAndInventory));
    }
    void DeletePlayerAndInventory()
    {
        PlayerPersistence.instance.GetComponent<PlayerMovement>().ChangePlayerState(false);
        InventoryPersistence.instance.gameObject.SetActive(false);

        LoadScore();

    }

    void LoadScore()
    {
        SceneManager.LoadScene(deadSceneId + 2, LoadSceneMode.Additive);
    }
    public void LoadScene(int sceneId)
    {
        onSceneFinishedLoading = false;
        StartCoroutine(LoadLevel(sceneId));
    }

    IEnumerator LoadLevel(int sceneId)
    {
        transitionAnimator.SetTrigger("End");

        yield return new WaitForSeconds(animationTime);
        StartCoroutine(LoadSceneAsync(sceneId));

    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        loadingBarBackground.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }

        transitionAnimator.SetTrigger("Start");

        loadingBarBackground.SetActive(false);
        onSceneFinishedLoading = true;
    }

    IEnumerator WaitForSceneLoaded(Action functionToCall)
    {
        while (!onSceneFinishedLoading)
        {
            yield return null;
        }
        functionToCall.Invoke();

    }
}
