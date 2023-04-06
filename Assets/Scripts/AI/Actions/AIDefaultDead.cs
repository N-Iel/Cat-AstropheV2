using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDefaultDead : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    bool destroy = false;

    // Start is called before the first frame update
    void Start()
    {
        if (destroy)
            Destroy(enemy);
        else
            enemy.SetActive(false);

    }
}
