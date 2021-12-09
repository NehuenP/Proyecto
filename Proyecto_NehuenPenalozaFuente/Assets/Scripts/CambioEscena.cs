using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void elegirMundo(int mundo)
    {
        if (mundo == -1)
        {
            SceneManager.LoadScene("Inicio");
        }
        else if (mundo < 2)
        {
            SceneManager.LoadScene("Dificultad");
            PlayerPrefs.SetInt("mundo", mundo);
        }
        else
        {
            SceneManager.LoadScene("Incompleto");
        }
        
    }

    public void reiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void continuarMundo()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        SceneManager.LoadScene("Mundo1");
    }

    public void inicio()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void close()
    {
        Application.Quit();
    }
}
