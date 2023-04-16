using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandomMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    [Range(1,10)]
    float minWaitTime;

    [SerializeField]
    [Range(2,10)]
    float maxMovementTime;

    [SerializeField]
    [Range(1, 9)]
    float minMovementTime;

    bool running = false;
    Vector2 moveDir;

    [Header("Components")]
    [SerializeField]
    EnemyActionMove movement;

    [SerializeField]
    Brain brain;


    private void Awake()
    {
        if (minMovementTime > maxMovementTime)
            minMovementTime = maxMovementTime;
    }

    public void StartCorutine()
    {
        if (running || brain.currentState != Constants.States.pasive) return;
        running = true;
        Invoke("RandomMovement",0);
        movement.maxSpeed = maxSpeed;
        Debug.Log("Started");
    }

    public void StopCorutine()
    {
        running = false;
        Debug.Log("finished");
    }

    void RandomMovement()
    {
        if (!running) return;
        moveDir = Random.insideUnitCircle.normalized;
        float time = 0;
        float moveTime = GetMovementTime();
        do
        {
            time += Time.deltaTime;
            movement.Move(moveDir);
        } while (time <= moveTime);
        Invoke("RandomMovement", GetWaitTime());
    }

    float GetMovementTime()
    {
        return Random.Range(minMovementTime, maxMovementTime);
    }
    
    float GetWaitTime()
    {
        return Random.Range(minWaitTime, minWaitTime * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            moveDir *= -1;
    }
}
