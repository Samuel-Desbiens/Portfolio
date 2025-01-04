using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
public class OldGameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    private const int firstGamingLevel = 1;
    private const int lastGamingLevel = 4;
    private int maxLives = 100;
    private int livesIncrement = 10;

    private int actualLevel = 0;

    private int score = 0;
    [SerializeField] private int lives;
    private int bonus = 0;

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    private bool isPaused = false;


    [SerializeField] private int powerMultiplicator = 1;
    //private int powerIncrement = 1;

    Text pauseText;
    Text playerScoreText;
    Text playerLivesText;
    Text playerBonusText;
    Text playerBestScoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        actualLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) Application.Quit();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        linkTexts();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        ShowPauseMessage();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; 
        isPaused = false;
        HidePauseMessage();
    }

    private void ShowPauseMessage()
    {
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(true); 
        }
    }

    private void HidePauseMessage()
    {
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(false); 
        }
    }

    private void linkTexts()
    {
        if (lives == 0) return;
        if (textsNotLinked)
        {
            textsNotLinked = false;
            if (actualLevel == 0 || actualLevel == 1) return;
            else if (actualLevel == 4)
            {
                playerScoreText = GameObject.FindGameObjectWithTag("TextScore").GetComponent<Text>();
                playerScoreText.text = "Your score " + getFinalScore().ToString();

                ScoreSave scoreSave = GetComponent<ScoreSave>();
                scoreSave.AddScoreToFile();

                playerBestScoreText = GameObject.FindGameObjectWithTag("TextBestScore").GetComponent<Text>();
                playerBestScoreText.text = "Your best score " + scoreSave.getScore().ToString();
                return;
            }

            playerLivesText = GameObject.FindGameObjectWithTag("TextLives").GetComponent<Text>();
            playerLivesText.text = lives.ToString();

            playerScoreText = GameObject.FindGameObjectWithTag("TextScore").GetComponent<Text>();
            playerScoreText.text = score.ToString();

            playerBonusText = GameObject.FindGameObjectWithTag("TextBonus").GetComponent<Text>();
            playerBonusText.text = bonus.ToString();

            pauseText = GameObject.FindGameObjectWithTag("TextPause").GetComponent<Text>();
            pauseText.gameObject.SetActive(false);
        }
    }

    public void StartNextlevel(float delay)
    {
        if (scenesAreInTransition) return; 

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GetNextLevel()));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;

        if (level == 1)
            SceneManager.LoadScene("Bonus Stage");
        else if (level == 2)
            SceneManager.LoadScene("TrueLevel");
        else if (level == 3)
            SceneManager.LoadScene("Boss");
        else
            SceneManager.LoadScene("SceneGameWon");
        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = 0;
        score = 0;
        SceneManager.LoadScene("Menu");
    }

    private int GetNextLevel()
    {
        if (++actualLevel == lastGamingLevel + 1)
            actualLevel = firstGamingLevel;

        return actualLevel;
    }

    //---------------------------------------------------------------
    //Role "traditionnel" du Game Manager: petit donc on le garde ici
    //---------------------------------------------------------------
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        playerScoreText.text = score.ToString();
    }

    public void PlayerTakeDamage(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            SceneManager.LoadScene("SceneGameOver");
        }
        else
        {
            playerLivesText.text = this.lives.ToString();
        }
    }

    public void SetBonus(int bonus)
    {
        this.bonus = bonus;

        if (playerBonusText == null) return;

        playerBonusText.text = this.bonus.ToString();
    }

    public int getBonus()
    {
        return bonus;
    }

    public int getScore()
    {
        return score;
    }

    public int getFinalScore()
    {
        return score + bonus;
    }

    public int getMultiplicator()
    {
        return powerMultiplicator;
    }

    public void addMultiplicator()
    {
        powerMultiplicator += 1;
    }

    public void addMaxLife()
    {
        maxLives+=livesIncrement;
    }
}*/