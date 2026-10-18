using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuraField : MonoBehaviour
{

    public Rigidbody2D rb;
    public float Speed = 5f;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 ||  movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
        
    }

    void FixedUpdate() 
    { 
        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);

    }
}
