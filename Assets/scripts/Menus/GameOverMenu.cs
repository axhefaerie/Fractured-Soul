using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public int escena;

    public void RespawnGame()
    {
        SceneManager.LoadScene(escena);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        //Application.Quit();
    }
}
