using Constants;
using RengeGames.HealthBars;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    Seasons initialSeason = Seasons.Spring;
    [SerializeField]
    [Range(0, 5)]
    int maxGoodEnemies;
    [SerializeField]
    [Range(0, 5)]
    int maxBadEnemies;
    [SerializeField]
    float goodAmountPerEnemy;
    [SerializeField]
    float badAmountPerEnemy;
    [SerializeField]
    float badAmountIncreaseAmount;
    [SerializeField]
    float badAmountIncreaseRate;

    [field: Header("Bars")]
    [field: SerializeField]
    public RadialSegmentedHealthBar goodBar { get; set; }
    [SerializeField]
    TextMeshProUGUI goodName;
    [SerializeField]
    TextMeshProUGUI goodNumber;
    [field: SerializeField]
    public RadialSegmentedHealthBar badBar { get; set; }
    [SerializeField]
    TextMeshProUGUI badName;
    [SerializeField]
    TextMeshProUGUI badNumber;

    [SerializeField]
    TextMeshProUGUI seasonText;

    public static SeasonManager seasonManager { get; private set; }
    List<Season> availableSeasons = new List<Season>();
    Enemies goodEnemy, badEnemy;
    int goodEnemyCount, badEnemyCount;

    // Randomness
    System.Random random = new System.Random();
    Array enemies, goodSeasons, badSeasons;

    private void Awake()
    {
        seasonManager = this;
    }

    private void OnEnable()
    {
        // Events
        EnemyHealth.onKill += OnEnemyKilled;

        // Lists
        enemies = Enum.GetValues(typeof(Enemies));
        goodSeasons = Enum.GetValues(typeof(GoodSeasons));
        badSeasons = Enum.GetValues(typeof(BadSeasons));

        foreach (GameObject season in GameObject.FindGameObjectsWithTag("Season"))
        {
            availableSeasons.Add(season.GetComponent<Season>());
        };

        // Initialization
        seasonText.text = initialSeason.ToString();
        ResetEnemyNumbers();
        TriggerNewSeason(initialSeason.ToString());
        StartCoroutine(IncreaseBadBar());
    }

    private void OnDisable()
    {
        EnemyHealth.onKill -= OnEnemyKilled;
    }

    void OnEnemyKilled(Enemies id)
    {
        if(id == goodEnemy)
        {
            // Subir Barra buena
            goodBar.AddRemoveSegments(-goodAmountPerEnemy);
            goodEnemyCount++;
            goodNumber.text = (maxGoodEnemies - goodEnemyCount).ToString();
        }
        else if(id == badEnemy)
        {
            // Subir velocidad barra mala
            badBar.AddRemoveSegments(-badAmountPerEnemy);
            badEnemyCount++;
            badNumber.text = (maxBadEnemies - badEnemyCount).ToString();
        }

        if (goodEnemyCount >= maxGoodEnemies || badEnemyCount >= maxBadEnemies)
            ResetEnemyNumbers();

        // Comprobar si se llenan las barras para cambiar de season
        if(badBar.RemoveSegments.Value <= 3 || goodBar.RemoveSegments.Value <= 3)
            OnSeasonChange(goodBar.RemoveSegments.Value == 3);
    }

    void ResetEnemyNumbers()
    {

        maxGoodEnemies = UnityEngine.Random.Range(1, 5);
        goodEnemy = (Enemies)enemies.GetValue(random.Next(enemies.Length));
        goodEnemyCount = 0;
        goodNumber.text = maxGoodEnemies.ToString();
        goodName.text = goodEnemy.ToString();
        // Actualizar número bueno

        maxBadEnemies = UnityEngine.Random.Range(1, 5);
        do badEnemy = (Enemies)enemies.GetValue(random.Next(enemies.Length)); while (badEnemy == goodEnemy);
        badEnemyCount = 0;
        badNumber.text = maxBadEnemies.ToString();
        badName.text = badEnemy.ToString();
        // Actualizar número malo
    }

    void OnSeasonChange(bool isGood)
    {
        string newSeason = seasonText.text;

        // Bar Managemenet
        if (isGood)
        {
            badBar.SetRemovedSegments(Mathf.Clamp(badBar.RemoveSegments.Value + 3, 3, 10));
            goodBar.SetRemovedSegments(10);
            do seasonText.text = goodSeasons.GetValue(random.Next(goodSeasons.Length)).ToString(); while (newSeason == seasonText.text);
        }
        else
        {
            goodBar.SetRemovedSegments(Mathf.Clamp(goodBar.RemoveSegments.Value + 3, 3, 10));
            badBar.SetRemovedSegments(10);
            do seasonText.text = badSeasons.GetValue(random.Next(badSeasons.Length)).ToString(); while (newSeason == seasonText.text);
        }

        StopAllCoroutines();
        TriggerNewSeason(seasonText.text);
        StartCoroutine(IncreaseBadBar());
    }

    void TriggerNewSeason(string _season)
    {
        Debug.Log(_season);
        availableSeasons.Find((season) => season.season.ToString() == _season).StartSeason();
    }

    IEnumerator IncreaseBadBar()
    {
        if (badBar.RemoveSegments.Value <= 3 || goodBar.RemoveSegments.Value <= 3)
            OnSeasonChange(goodBar.RemoveSegments.Value == 3);

        badBar.AddRemoveSegments(-badAmountIncreaseAmount);
        yield return new WaitForSeconds(badAmountIncreaseRate);
        StartCoroutine(IncreaseBadBar());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            OnEnemyKilled(goodEnemy);

        if (Input.GetKeyDown(KeyCode.X))
            OnEnemyKilled(badEnemy);

        if (Input.GetKeyDown(KeyCode.R))
            ResetEnemyNumbers();
    }
}
