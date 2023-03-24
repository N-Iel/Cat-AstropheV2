using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script usado para spawnear buhos, necesitará un array con los arboles del mapa y de manera aleatoria irá spawneando buhos en ellos.
/// Se establecerá un limite de buhos por parámetro, el tiempo de spawn entre uno y otro
/// Los buhos yá estarán creados para evitar problemas de rendimiento destruyendolos.
/// </summary>
public class OwlSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> owls;

    [SerializeField]
    int maxOwlsSpawned = 1;

    [SerializeField]
    [Range(1, 5)]
    float initialspawnDelay = 4;

    [SerializeField]
    [Range(1,100)]
    float spawnDelay = 1;

    private void Start()
    {
        if (owls.Count <= 0) { Debug.Log("No Owls available"); return;};
        if (maxOwlsSpawned > owls.Count) maxOwlsSpawned = owls.Count;
        InvokeRepeating("SpawnOwl", initialspawnDelay, spawnDelay);
    }

    private void SpawnOwl()
    {
        if (!CheckAvailableOwls()) return;

        bool owlSpawned = false;
        while (!owlSpawned)
        {
            int index = Random.Range(0, owls.Count - 1);
            if (!owls[index].activeInHierarchy)
            {
                // Poner un delay al desactivar de los buhos para evitar que spawnee justo un buho recién derrotado
                owls[index].SetActive(true);
                owlSpawned = true;
            }
        }
    }

    private bool CheckAvailableOwls()
    {
        int activeOwls = 0;
        foreach (GameObject owl in owls)
        {
            if (owl.activeInHierarchy) activeOwls++;
            if (activeOwls >= maxOwlsSpawned) break;
        }

        // If the number of active owls is equal to the capacity or the number of owls it wont spawn more
        return activeOwls < owls.Count && activeOwls < maxOwlsSpawned;
    }
}
