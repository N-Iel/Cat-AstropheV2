using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreDefinedSpawner : MonoBehaviour
{
    

    [field: Header("Spawn params")]
    [field: SerializeField]
    [Tooltip("If spawnPoints are introduced, spawnOffset and radius will be ignored")]
    List<GameObject> spawnPoints;
    [field: SerializeField]
    public float spawnRate { get; set; }
    [field: SerializeField]
    public float spawnLimit { get; set; }

    [SerializeField]
    [Range(0f, 5f)]
    float spawnOffset;

    [SerializeField]
    float radius;

    [Header("Spawn pool")]
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject parent;

    // Enemies will be pooled in order to improve preformance
    List<GameObject> enemies = new List<GameObject>();

    private void OnDisable()
    {
        parent.transform.MMDestroyAllChildren();
    }

    // Start is called before the first frame update
    void Start()
    {
        FillPool();
        InvokeRepeating("Spawn", 0, spawnRate);
    }

    void Spawn()
    {
        // Spawn limit check
        if (enemies.FindAll((enemy) => enemy.activeInHierarchy).Count >= spawnLimit) return;

        GameObject _enemy = enemies.Find(enemy => !enemy.activeInHierarchy);

        if (!_enemy) { FillPool(); return; }
        if (spawnPoints.Count > 0)
            StaticSpawn(_enemy);
        else
            RandomSpawn(_enemy);

        _enemy.SetActive(true);
    }

    // Used for spawnPoint
    void StaticSpawn(GameObject _enemy)
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
    void RandomSpawn(GameObject _enemy)
    {
        Vector2 spawnPos = Vector2.zero;
        do
        {
            float angle = Random.Range(1f, enemies.Count + 1) * Mathf.PI * 2f / enemies.Count;
            spawnPos = (Vector2)Player.player.transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

        } while (Utils.isOnWall(spawnPos));
        _enemy.transform.position = spawnPos;
    }

    void FillPool()
    {
        Debug.Log("Enemies Added");
        while (enemies.Count < spawnLimit)
        {
            GameObject _enemy = Instantiate(prefab, Vector2.zero, Quaternion.identity, parent.transform);
            _enemy.SetActive(false);
            enemies.Add(_enemy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Player.player.transform.position, radius);
    }
}
