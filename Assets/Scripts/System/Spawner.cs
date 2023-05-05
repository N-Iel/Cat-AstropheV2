using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    // Base
    public abstract Enemies EnemyType { get; set; }
    public abstract GameObject enemyPrefab { get; set; }
    public abstract GameObject spawnParent { get; set; }
    public abstract int spawnLimit { get; set; }
    public abstract float spawnRate  { get; set; }
    public abstract float initialSpawnDelay { get; set; }

    // RandomSpawn
    public abstract float spawnRadius { get; set; }
    public abstract float spawnOffset { get; set; }

    // DefinedSpawn
    public abstract List<GameObject> spawnPoints { get; set; }

    public abstract void Spawn();
    public abstract void StaticSpawn(GameObject _enemy);
    public abstract void RandomSpawn(GameObject _enemy);
    public abstract void FillPool();
}
