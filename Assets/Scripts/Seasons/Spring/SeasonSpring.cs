using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSpring : Season
{
    // Base params
    [field: Header("Params")]
    [field: SerializeField]
    public override float goal { get; set; }
    [field: SerializeField]
    public override float count { get; set; }
    [field: SerializeField]
    public override Seasons season { get; set; }

    [field: SerializeField]
    public override Seasons goodSeason { get; set; }
    [field: SerializeField]
    public override Seasons badSeason { get; set; }

    // Custom params
    [SerializeField]
    float flowerSpawnRate = 3f;

    [SerializeField]
    Transform flowerParent;

    // Objetives
    List<GameObject> flowers = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform flower in flowerParent)
        {
            flowers.Add(flower.gameObject);
        }
    }

    public override void StartSeason()
    {
        InvokeRepeating("generateFlowers", 0, flowerSpawnRate);
        Flower.flowercollected += CheckObjetive;
        count = 0;
    }

    public override void StopSeason()
    {
        CancelInvoke();
        Flower.flowercollected -= CheckObjetive;

        foreach (GameObject flower in flowers)
            flower.gameObject.SetActive(false);
    }

    void generateFlowers()
    {
        List<GameObject> availableFlowers = flowers.FindAll((flower) => !flower.activeInHierarchy);
        if (availableFlowers.Count <= 0 || availableFlowers.Count <= flowers.Count - goal) return;

        GameObject flower = availableFlowers[Random.Range(0, availableFlowers.Count)];
        flower.transform.localPosition = new Vector2(
                                            Random.Range(flower.transform.localPosition.x, flower.transform.localPosition.x + 2),
                                            Random.Range(flower.transform.localPosition.y, flower.transform.localPosition.y + 2));
        flower.SetActive(true);
    }

    public override void CheckObjetive()
    {
        TriggerEvent();
        count++;
    }
}
