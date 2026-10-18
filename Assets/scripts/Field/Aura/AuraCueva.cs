using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuraCueva : MonoBehaviour
{

    public Rigidbody2D rb;
    public float Speed = 5f;
    public Animator animator;

    Vector2 movement;

    public List<EnemyMovementField> enemigos;
    
    void Awake()
    {
        if (PlayerPrefs.GetInt("respawn") != 0)
        {
            if (PlayerPrefs.GetInt("HeGanado") == 1)
            {
                PlayerPrefs.SetInt(enemigos[PlayerPrefs.GetInt("Enemigo")].gameObject.name, 1);
            }
        }
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("respawn") == 0)
        {
            PlayerPrefs.SetFloat("PositionX", this.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("PositionY", this.gameObject.transform.position.y);
            PlayerPrefs.SetInt("respawn", 1);
        }
        else if (PlayerPrefs.GetInt("respawn") == 1)
        {
            if (PlayerPrefs.GetInt("HeGanado") == 1)
            {
                this.gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("PosicionX"), PlayerPrefs.GetFloat("PosicionY"));
            }

            else if (PlayerPrefs.GetInt("HeGanado") == 0)
            {
                this.gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
            }
            
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemigo"))
        {
            PlayerPrefs.SetFloat("PosicionX", this.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("PosicionY", this.gameObject.transform.position.y);

            PlayerPrefs.SetInt("Enemigo", enemigos.IndexOf(collision.gameObject.GetComponent<EnemyMovementField>()));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("reliquia"))
        {
            PlayerPrefs.SetFloat("PositionX", this.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("PositionY", this.gameObject.transform.position.y);
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("respawn", 0);
    }
}
