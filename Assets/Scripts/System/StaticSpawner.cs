using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemies;

    [SerializeField]
    int maxEnemiesSpawned = 1;

    [SerializeField]
    [Range(1, 5)]
    float initialspawnDelay = 4;

    [SerializeField]
    [Range(1,100)]
    float spawnDelay = 1;

    private void OnDisable()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void Start()
    {
        if (enemies.Count <= 0) { Debug.Log("No Enemies available"); return;};
        if (maxEnemiesSpawned > enemies.Count) maxEnemiesSpawned = enemies.Count;
        InvokeRepeating("Spawn", initialspawnDelay, spawnDelay);
    }

    private void Spawn()
    {
        if (!CheckAvailable()) return;

        bool owlSpawned = false;
        while (!owlSpawned)
        {
            int index = Random.Range(0, enemies.Count - 1);
            if (!enemies[index].activeInHierarchy)
            {
                // Poner un delay al desactivar de los buhos para evitar que spawnee justo un enemigo recién derrotado
                enemies[index].SetActive(true);
                owlSpawned = true;
            }
        }
    }

    private bool CheckAvailable()
    {
        int activeOwls = 0;
        foreach (GameObject owl in enemies)
        {
            if (owl.activeInHierarchy) activeOwls++;
            if (activeOwls >= maxEnemiesSpawned) break;
        }

        // If the number of active owls is equal to the capacity or the number of enemies it wont spawn more
        return activeOwls < enemies.Count && activeOwls < maxEnemiesSpawned;
    }
}
