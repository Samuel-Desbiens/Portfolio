using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int Score;
    int Life;
    int Multiplier;

    int ScoreGoal;

    Vector3 MainBallPos;

    List<GameObject> ActiveLights;

    [SerializeField] private GameObject prefabBall;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Life = 3;
        Multiplier = 1;

        ScoreGoal = 2500;

        ActiveLights = new List<GameObject> { };
    }

    private void Update()
    {
        if(Score >= ScoreGoal)
        {
            Life++;
            ScoreGoal *= 2;
        }
    }

    public void LoseLife()
    {
        Life--;
        if (Life >= 0)
        {
            Instantiate(prefabBall, MainBallPos, Quaternion.identity);

            foreach(GameObject light in ActiveLights)
            {
                light.GetComponent<LightsScript>().ResetLights();
            }
            Multiplier = 1;
        }
        else
        {
            //Code GameOver
        }

    }

    public void Addpoints(int PointsToAdd)
    {
        Score += PointsToAdd * Multiplier;
    }

    public void AddMultiplier(int MultiplierAdd)
    {
        Multiplier *= MultiplierAdd;
    }

    public void GetMainBallPos(Vector3 Pos)
    {
        MainBallPos = Pos;
    }

    public void AddActiveLights(GameObject Light)
    {
        ActiveLights.Add(Light);
    }

    public bool GetJackpotOn()
    {
        return Multiplier >= 11;
    }

    public int[] GetGameInfos()
    {
        int[] Infos = new int[3] { Score, Multiplier, Life };
        return Infos;
    }
}
