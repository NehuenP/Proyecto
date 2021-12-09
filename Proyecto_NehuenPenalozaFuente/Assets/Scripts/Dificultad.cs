using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dificultad : MonoBehaviour
{
    public Text texto;
    private int mundo;
    // Start is called before the first frame update
    void Start()
    {
        reinicioPrefs();
        mundo = PlayerPrefs.GetInt("mundo");
        GameObject go = GameObject.Find("Mundo");
        texto.text = "Mundo "+mundo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void elegirDificultad(string dificultad) {
        PlayerPrefs.SetString("dificultad", dificultad);
        SceneManager.LoadScene("Mundo" + mundo);
    }

    private void reinicioPrefs()
    {
        PlayerPrefs.SetInt("aciertos", 0);
        PlayerPrefs.SetInt("Estaciones", 0);
        PlayerPrefs.SetInt("Emociones", 0);
    }
}
