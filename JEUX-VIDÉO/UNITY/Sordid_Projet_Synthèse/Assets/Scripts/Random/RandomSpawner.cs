using UnityEngine;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    private List<GameObject> spawnersList = new List<GameObject>();

    [SerializeField] private int nbMinOfSpawners;

    void Start()
    {
        GameObject[] allSpawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject sapwner in allSpawners)
        {
            spawnersList.Add(sapwner);
        }

        ShowRandomSpawners();
    }

    void ShowRandomSpawners()
    {
        List<GameObject> activeSpawners = new List<GameObject>();

        foreach (GameObject spawner in spawnersList)
        {
            if (spawner.activeSelf)
            {
                activeSpawners.Add(spawner);
                spawner.SetActive(false);
            }
        }

        int totalActiveSpawners = activeSpawners.Count;

        int nbToActivate = Random.Range(nbMinOfSpawners, totalActiveSpawners + 1);

        List<int> selectedIndexes = new List<int>();

        for (int i = 0; i < nbToActivate; i++)
        {
            int index = Random.Range(0, totalActiveSpawners);

            while (selectedIndexes.Contains(index))
            {
                index = Random.Range(0, totalActiveSpawners);
            }

            selectedIndexes.Add(index);

            GameObject spawnerToActivate = activeSpawners[index];

            spawnerToActivate.SetActive(true);
        }
    }

}
