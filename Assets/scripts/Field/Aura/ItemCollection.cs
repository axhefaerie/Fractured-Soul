using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public Animator animator;

    private int esen = 0;
    private int relic = 0;

    [SerializeField]
    private Text esenciasText;

    void Awake()
    {
        if (PlayerPrefs.GetInt("respawn") == 0)
        {
            PlayerPrefs.SetInt("Esencias", 0);
            if (PlayerPrefs.GetInt("Esencias") == 0)
            {
                esenciasText.text = "Esencias = " + esen;
            }
            PlayerPrefs.SetInt("Reliquias", 0);
        }

        if (PlayerPrefs.GetInt("respawn") == 1)
        {
            esen = PlayerPrefs.GetInt("Esencias");
            esenciasText.text = "Esencias = " + esen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("esencias"))
        {
            Destroy(collision.gameObject);
            esen++;
            esenciasText.text = "Esencias = " + esen;
            PlayerPrefs.SetInt("Esencias", esen);
            PlayerPrefs.SetInt(collision.gameObject.name, 1);
        }

        if (collision.gameObject.CompareTag("reliquia"))
        {
            animator.SetBool("pickRelic", true);
            Destroy(collision.gameObject, 2);
            PlayerPrefs.SetInt("Reliquias", relic);
            PlayerPrefs.SetInt(collision.gameObject.name, 1);
            Invoke("FinishRelic", 2);
        }
    }

    void FinishRelic()
    {
        animator.SetBool("pickRelic", false);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("respawn", 0);
    }
}
