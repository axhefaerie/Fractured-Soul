using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilithField : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    public Transform player;
    public Vector2 initialPosition;
    public Animator animator;

    public float playerAwarenessDistance;

    private void Awake()
    {
        player = FindObjectOfType<AuraField>().transform;
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        //DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
            animator.SetBool("desaparecer", false);
        }
        else
        {
            AwareOfPlayer = false;
            animator.SetBool("desaparecer", true);
            Invoke("Desaparecer", 0.04f);
        }
    }

    void Desaparecer()
    {
        gameObject.SetActive(false);
    }
}


