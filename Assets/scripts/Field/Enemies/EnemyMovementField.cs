using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementField : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private PlayerAwareness pac;
    private Vector2 targetDirection;
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pac = GetComponent<PlayerAwareness>();

        if (PlayerPrefs.GetInt("respawn") != 0)
        {
            if (PlayerPrefs.GetInt(this.gameObject.name) == 1)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt(this.gameObject.name, 0);
        }
    }
    
    void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
    }

    public void UpdateTargetDirection()
    {
        animator.SetFloat("Horizontal", targetDirection.x);
        
        if (pac.AwareOfPlayer)
        {
            targetDirection = pac.DirectionToPlayer;
        }
        else
        {
            targetDirection = pac.DirectionToSpawn;
        }
    }

    public void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = targetDirection * speed;
        }
        if (pac.AwareOfPlayer)
        {
            if (Vector2.Distance(transform.position, pac.player.position)< 0.07f)
            {
               rb.velocity = Vector2.zero;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, pac.initialPosition) < 0.07f)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }


}
