using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// enemy spawner will instantiate enemies arround the player, out of their field of view
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [field: Header("Spawn params")]
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

        if (!_enemy){ FillPool(); return;}

        float angle = Random.Range(1, enemies.Count + 1) * Mathf.PI * 2f / enemies.Count;
        _enemy.transform.position = (Vector2)Player.player.transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        _enemy.SetActive(true);
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
