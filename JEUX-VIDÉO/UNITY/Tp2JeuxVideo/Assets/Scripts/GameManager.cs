using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool DebugState;

    [SerializeField] Text BlueWin;
    [SerializeField] Text GreenWin;
    [SerializeField] Text Numbers;

    GameObject WizardContainer;
    GameObject BulletContainer;

    List<GameObject> GreenTowers;
    List<GameObject> BlueTowers;

    const int MaxNBWizardByTeam = 20;
    const int MaxNBBulletByTeam = 40;

    const int WizardTimerCheck = 150;
    int WizardTimer;

    bool GreenSpawn;
    bool BlueSpawn;

    EndGameManager endGameManager;
    bool end;

    //Doit mettre NbType a la fin pour avoir le nombre d'équipe.
    enum Teams
    {
        Green,
        Blue,
        NBType
    }

    void Awake()
    {
        WizardContainer = GameObject.Find("Wizards");
        BulletContainer = GameObject.Find("Bullets");

        endGameManager = GameObject.Find("Wizards").GetComponent<EndGameManager>();

        GreenTowers = new List<GameObject>();
        BlueTowers = new List<GameObject>();

        for (int i = 0; i<(int)Teams.NBType;i++)
        {
            for (int o = 0; o < MaxNBWizardByTeam; o++)
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Wizard"), Vector3.zero, Quaternion.identity);

                go.name = "Wizard" + (Teams)i;
                go.transform.parent = WizardContainer.transform.GetChild(i);

                go.SetActive(false);

               
            }
            for(int o = 0;o < MaxNBBulletByTeam;o++)
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Projectile"), Vector3.zero, Quaternion.identity);

                go.name = "Bullet" + (Teams)i;
                go.transform.parent = BulletContainer.transform.GetChild(i);

                go.SetActive(false);
            }
        }

        WizardTimer = 0;

        GreenSpawn = false;
        BlueSpawn = false;

        end = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!end)
        {
            WizardTimer++;
            if (WizardTimer >= WizardTimerCheck)
            {
                for (int i = 0; i < WizardContainer.transform.childCount; i++)
                {
                    for (int o = 0; o < WizardContainer.transform.GetChild(i).childCount; o++)
                    {
                        if (!WizardContainer.transform.GetChild(i).GetChild(o).gameObject.activeSelf)
                        {
                            if (i == 0 && !GreenSpawn)
                            {

                                WizardContainer.transform.GetChild(i).GetChild(o).position = GreenTowers[Random.Range(0, GreenTowers.Count)].transform.position;
                                WizardContainer.transform.GetChild(i).GetChild(o).gameObject.SetActive(true);
                                WizardContainer.transform.GetChild(i).GetChild(o).GetComponent<WizardState>().SetTarget(BlueTowers[Random.Range(0, BlueTowers.Count)]);
                                GreenSpawn = true;
                                break;
                            }
                            else if (!BlueSpawn)
                            {
                                WizardContainer.transform.GetChild(i).GetChild(o).position = BlueTowers[Random.Range(0, BlueTowers.Count)].transform.position;
                                WizardContainer.transform.GetChild(i).GetChild(o).gameObject.SetActive(true);
                                WizardContainer.transform.GetChild(i).GetChild(o).GetComponent<WizardState>().SetTarget(GreenTowers[Random.Range(0, GreenTowers.Count)]);
                                BlueSpawn = true;
                                break;
                            }
                        }
                    }
                }

                if (GreenSpawn && BlueSpawn)
                {
                    WizardTimer = 0;

                    GreenSpawn = false;
                    BlueSpawn = false;
                }
            }
        }
        int NbGreen = 0;
        int NbBlue = 0;
        for(int i =0;i<WizardContainer.transform.childCount;i++)
        {
            for(int o = 0; o<WizardContainer.transform.GetChild(i).transform.childCount;o++)
            {
                if(WizardContainer.transform.GetChild(i).transform.GetChild(o).gameObject.activeSelf)
                {
                    if (i == 0)
                    {
                        NbGreen++;
                    }
                    else
                    {
                        NbBlue++;
                    }
                }  
            }
        }

        Numbers.text = NbGreen + "                                                                                                         " + NbBlue;
    }

    private void GameOverManager(bool Winner)
    {
        end = true;
        if (Winner)
        {
            GreenWin.gameObject.SetActive(true);
        }
        else
        {
            BlueWin.gameObject.SetActive(true);
        }
        endGameManager.EndGameBroadcast();
    }

    public void AddTower(GameObject TowerToAdd)
    {
        if(TowerToAdd.GetComponent<TowersBehaviour>().GetTeam())
        {
            GreenTowers.Add(TowerToAdd);
        }
        else
        {
            BlueTowers.Add(TowerToAdd);
        }
    }

    public void RemoveTower(GameObject TowerToRemove)
    {
        if(GreenTowers.Contains(TowerToRemove))
        {
            GreenTowers.Remove(TowerToRemove);
            if(GreenTowers.Count <= 0)
            {
                GameOverManager(false);
            }
        }
        else if(BlueTowers.Contains(TowerToRemove))
        {
            BlueTowers.Remove(TowerToRemove);
            if (BlueTowers.Count <= 0)
            {
                GameOverManager(true);
            }
        }
        else
        {
            Debug.Log("TowerNotFoundInList");
        }
    }

    public GameObject GetRandomEnemyTower(bool team)
    {
        if(team)
        {
            if(BlueTowers.Count>0)
            {
                return BlueTowers[Random.Range(0, BlueTowers.Count)];
            }
        }
        else if(GreenTowers.Count>0)
        {
            return GreenTowers[Random.Range(0, GreenTowers.Count)];
        }
        return null;
    }

    public bool GetDebugState()
    {
        return DebugState;
    }
}
