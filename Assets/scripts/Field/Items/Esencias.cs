using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esencias : MonoBehaviour
{
    void Awake()
    {
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
}
