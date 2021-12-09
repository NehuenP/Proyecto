using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarJuego : MonoBehaviour
{
    public string juego;
    void OnMouseDown()
    {
        if (Time.timeScale == 1)
        {
            if (juego.Equals("Estaciones"))
            {
                PlayerPrefs.SetString("EstacionesCompletadas", "");
            }
            SceneManager.LoadScene(juego);
        }
    }
}
