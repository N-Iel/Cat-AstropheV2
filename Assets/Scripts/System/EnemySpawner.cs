using Constants;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [field: Header("Base Params")]
    [field: SerializeField]
    public override Enemies EnemyType { get; set; }
    [field: SerializeField]
    public override GameObject enemyPrefab { get; set; }
    [field: SerializeField]
    public override GameObject spawnParent { get; set; }
    [field: SerializeField]
    public override int spawnLimit { get; set; }
    [field: SerializeField]
    public override float spawnRate { get; set; }
    [field: SerializeField]
    public override float initialSpawnDelay { get; set; }
    [SerializeField]
    bool showGizmos = false;


    [field: Header("Random Spawn")]
    [field: SerializeField]
    public override float spawnRadius { get; set; }
    [field: SerializeField]
    public override float spawnOffset { get; set; }

    [field: Header("Defined Spawn")]
    [field: SerializeField]
    public override List<GameObject> spawnPoints { get; set; }

    // Enemies will be pooled in order to improve preformance
    List<GameObject> enemies = new List<GameObject>();

    private void OnDisable()
    {
        try { spawnParent.transform.MMDestroyAllChildren(); } catch { throw; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //FillPool();
        InvokeRepeating("Spawn", initialSpawnDelay, spawnRate);
    }

    public override void Spawn()
    {
        GameObject _enemy;

        if (spawnLimit != -1 && enemies.FindAll((enemy) => enemy.activeInHierarchy).Count >= spawnLimit) return;

        do
        {
            FillPool();
            _enemy = enemies.Find(enemy => !enemy.activeInHierarchy);
        } while (!_enemy);

        if (spawnPoints.Count > 0)
            StaticSpawn(_enemy);
        else
            RandomSpawn(_enemy);

        _enemy.SetActive(true);
    }

    // Used for spawnPoint
    public override void StaticSpawn(GameObject _enemy)
    {
        // filtrar spawns sin hijos
        List<GameObject> availableSpawnPoint = spawnPoints.FindAll(point => point.transform.childCount == 0);
        // Con la length coger un index aleatorio
        int index = Random.Range(0, availableSpawnPoint.Count);
        // Asignar el enemigo a ese spawnpoint
        _enemy.transform.SetParent(availableSpawnPoint[index].transform);
        _enemy.transform.localPosition = Vector2.zero;
    }

    // Used for randomSpawns
    public override void RandomSpawn(GameObject _enemy)
    {
        Vector2 spawnPos;
        do
        {
            float angle = Random.Range(1f, enemies.Count + 1) * Mathf.PI * 2f / enemies.Count;
            spawnPos = (Vector2)Player.player.transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;

        } while (Utils.isOnWall(spawnPos));
        _enemy.transform.position = spawnPos;
    }

    public override void FillPool()
    {            
        while (enemies.Count < spawnLimit || spawnLimit == -1)
        {
            GameObject _enemy = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity, spawnParent.transform);
            _enemy.SetActive(false);
            enemies.Add(_enemy);

            if (spawnLimit == -1)
                return;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.DrawWireSphere(Player.player.transform.position, spawnRadius);
    }
}
