using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }
    public Vector2 DirectionToSpawn { get; private set; }

    public Vector3 initialPosition;
    public Transform player;

    public float playerAwarenessDistance;

    private void Awake()
    {
        player = FindObjectOfType<AuraField>().transform;
        initialPosition = transform.position; 
    }

    void Update ()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        Vector2 enemyToSpawn = initialPosition - transform.position;
        DirectionToSpawn = enemyToSpawn.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
