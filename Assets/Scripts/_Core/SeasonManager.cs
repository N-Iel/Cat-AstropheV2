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
    [Range(0, 5)]
    int maxGoodEnemies;
    [SerializeField]
    [Range(0, 5)]
    int maxBadEnemies;
    [SerializeField]
    float goodAmountPerEnemy;
    [SerializeField]
    float badAmountPerEnemy;

    [Header("Bars")]
    [SerializeField]
    RadialSegmentedHealthBar goodBar;
    [SerializeField]
    TextMeshProUGUI goodName;
    [SerializeField]
    TextMeshProUGUI goodNumber;
    [SerializeField]
    RadialSegmentedHealthBar badBar;
    [SerializeField]
    TextMeshProUGUI badName;
    [SerializeField]
    TextMeshProUGUI badNumber;

    [SerializeField]
    TextMeshProUGUI seasonText;

    Enemies goodEnemy, badEnemy;
    int goodEnemyCount, badEnemyCount;
    System.Random random = new System.Random();
    Array enemies, goodSeasons, badSeasons;

    private void OnEnable()
    {
        EnemyHealth.onKill += OnEnemyKilled;
        enemies = Enum.GetValues(typeof(Enemies));
        goodSeasons = Enum.GetValues(typeof(GoodSeasons));
        badSeasons = Enum.GetValues(typeof(BadSeasons));
        seasonText.text = GoodSeasons.Spring.ToString();
        ResetEnemyNumbers();
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
        if (isGood)
        {
            badBar.SetRemovedSegments(Mathf.Clamp(badBar.RemoveSegments.Value - 3, 3, 10));
            goodBar.SetRemovedSegments(10);
            do seasonText.text = goodSeasons.GetValue(random.Next(goodSeasons.Length)).ToString(); while (newSeason == seasonText.text);
        }
        else
        {
            goodBar.SetRemovedSegments(Mathf.Clamp(goodBar.RemoveSegments.Value - 3, 3, 10));
            badBar.SetRemovedSegments(10);
            do seasonText.text = badSeasons.GetValue(random.Next(badSeasons.Length)).ToString(); while (newSeason == seasonText.text);
        }
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
