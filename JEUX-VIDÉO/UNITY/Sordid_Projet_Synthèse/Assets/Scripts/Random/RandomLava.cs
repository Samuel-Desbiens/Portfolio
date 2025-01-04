using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLava : MonoBehaviour
{
    private List<GameObject> lavasList = new List<GameObject>();

    void Start()
    {
        GameObject[] allLavas = GameObject.FindGameObjectsWithTag("Lava");

        foreach (GameObject room in allLavas)
        {
            lavasList.Add(room);
        }

        ShowRandomLavas();
    }

    void ShowRandomLavas()
    {
        List<GameObject> activeLavas = new List<GameObject>();

        foreach (GameObject lava in lavasList)
        {
            if (lava.activeSelf)
            {
                activeLavas.Add(lava);
                lava.SetActive(false);
            }
        }

        int totalActiveLavas = activeLavas.Count;

        int nbToActivate = Random.Range(0, totalActiveLavas + 1);

        List<int> selectedIndexes = new List<int>();

        for (int i = 0; i < nbToActivate; i++)
        {
            int index = Random.Range(0, totalActiveLavas);

            while (selectedIndexes.Contains(index))
            {
                index = Random.Range(0, totalActiveLavas);
            }

            selectedIndexes.Add(index);

            GameObject lavaToActivate = activeLavas[index];

            lavaToActivate.SetActive(true);
        }
    }
}
