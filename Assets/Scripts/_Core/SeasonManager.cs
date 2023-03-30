using Constants;
using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    [SerializeField]
    RadialSegmentedHealthBar goodBar;
    [SerializeField]
    RadialSegmentedHealthBar badBar;
    [SerializeField]
    [Range(0, 5)]
    int maxGoodEnemies, maxBadEnemies;

    Enemies goodEnemy, badEnemy;
    int goodEnemyCount, badEnemyCount;

    private void OnEnable()
    {
        EnemyHealth.onKill += OnEnemyKilled;
    }

    void OnEnemyKilled(Enemies id)
    {
        if(id == goodEnemy)
        {
            // Subir Barra buena
            goodEnemyCount++;

        }
        else if(id == badEnemy)
        {
            // Subir velocidad barra mala
            badEnemyCount++;
        }

        if(goodEnemyCount >= maxGoodEnemies || badEnemyCount >= maxBadEnemies)
            ResetEnemyNumbers();

        // Comprobar si se llenan las barras para cambiar de season
    }

    void ResetEnemyNumbers()
    {
        maxGoodEnemies = Random.Range(0, 5);
        goodEnemyCount = 0;

        maxBadEnemies = Random.Range(0, 5);
        badEnemyCount = 0;
    }

    void OnSeasonChange()
    {

    }
}
