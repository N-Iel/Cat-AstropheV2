using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSpring : Season
{
    public override Seasons season { get; set; }

    [SerializeField]
    float flowerRate = 3f;
    [SerializeField]
    int flowerLimit = 3;
    List<GameObject> flowers = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform flower in transform)
        {
            flowers.Add(flower.gameObject);
        }
        Debug.Log($"Flowers: {flowers.Count}");
    }

    public override void StartSeason()
    {
        InvokeRepeating("generateFlowers", 0, flowerRate);
    }

    public override void StopSeason()
    {
        CancelInvoke();
    }

    void generateFlowers()
    {
        List<GameObject> availableFlowers = flowers.FindAll((flower) => !flower.activeInHierarchy);
        if (availableFlowers.Count <= 0 || availableFlowers.Count <= flowers.Count - flowerLimit) return;

        GameObject flower = availableFlowers[Random.Range(0, availableFlowers.Count)];
        flower.transform.localPosition = new Vector2(
                                            Random.Range(flower.transform.localPosition.x, flower.transform.localPosition.x + 2),
                                            Random.Range(flower.transform.localPosition.y, flower.transform.localPosition.y + 2));
        flower.SetActive(true);
    }
}
