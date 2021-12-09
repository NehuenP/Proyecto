using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Estaciones : MonoBehaviour
{
    private int estacion;
    public List<GameObject> estaciones;
    private string nombreEstacion;
    public float cTime;//Tiempo que pasa entre mensaje y mensaje
    public float cTime2;//Tiempo que dura el mensaje
    private int aciertosNecesarios;
    private string[] frasesAnimo = new string[] {"¡Solo queda una!","¡Tú puedes!","¡Ánimo!","¡Vas muy bien!", "¡Sigue así!", "¡Adelante!", "¡Genial!"};
    public Text textoAnimo;
    private bool animando;
    void Start()
    {
        PlayerPrefs.SetInt("aciertos", 0);
        //fondo cambia con estación estación aleatoria
        do
        {
            estacion = Random.Range(0, 4);
            nombreEstacion = estaciones[estacion].name;
        } while (PlayerPrefs.GetString("EstacionesCompletadas").Contains(nombreEstacion));
        estaciones[estacion].SetActive(true);
        cTime = 31; 
        cTime2 = 0;
        if (nombreEstacion == "invierno")
        {
            aciertosNecesarios = 5;
        }
        else if (nombreEstacion == "otonio" || nombreEstacion == "primavera"){
            aciertosNecesarios = 3;
        }
        else if (nombreEstacion == "verano")
        {
            aciertosNecesarios = 4;
        }
        animando = false;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("aciertos")==aciertosNecesarios)
        {
            Victoria();
        }
        if (cTime2 > 0)
        {
            cTime2 -= Time.deltaTime * 2;
        }
        else
        {
            if (animando)
            {
                GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
                if (fooGroup.Length > 0)
                {
                    foreach (GameObject go in fooGroup)
                    {
                        if (go.name.Equals("Animo"))
                        {
                            go.SetActive(false);
                            animando = false;
                        }
                    }
                }
            }
            
        }
        if (PlayerPrefs.GetInt("aciertos") == aciertosNecesarios - 1)
        {
            cTime = 31;
            cTime2 = 6;
            Animar(frasesAnimo[0]);
        }
        if (cTime > 0)
        {
            cTime-= Time.deltaTime*2;
        }
        else
        {
            cTime = 31;
            cTime2 = 6;
            Animar(frasesAnimo[Random.Range(1, frasesAnimo.Length)]);
        }
        
    }

    private void Animar(string message)
    {
        GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        if (fooGroup.Length > 0)
        {
            foreach (GameObject go in fooGroup)
            {
                if (go.name.Equals("Animo"))
                {
                    go.SetActive(true);
                    textoAnimo.text = message;
                    animando = true;
                }
            }
        }
    }

    private void Victoria()
    {
        PlayerPrefs.SetString("EstacionesCompletadas", PlayerPrefs.GetString("EstacionesCompletadas")+ nombreEstacion);
        if (PlayerPrefs.GetString("dificultad") == "Facil"){
            pantallaVictoria();
        }
        else if (PlayerPrefs.GetString("dificultad") == "Medio")
        {
            int i = 0;
            foreach(GameObject estacion in estaciones)
            {
                if (PlayerPrefs.GetString("EstacionesCompletadas").Contains(estacion.name))
                {
                    i++;
                }
            }
            if (i == 2)
            {
                pantallaVictoria();
            }
            else
            {
                SceneManager.LoadScene("Estaciones");
            }
        }
        else if (PlayerPrefs.GetString("dificultad") == "Dificil")
        {
            int i = 0;
            foreach (GameObject estacion in estaciones)
            {
                if (PlayerPrefs.GetString("EstacionesCompletadas").Contains(estacion.name))
                {
                    i++;
                }
            }
            if (i == estaciones.Count)
            {
                pantallaVictoria();
            }
            else
            {
                SceneManager.LoadScene("Estaciones");
            }
        }
        
    }

    private void pantallaVictoria()
    {
        GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        if (fooGroup.Length > 0)
        {
            foreach (GameObject go in fooGroup)
            {
                if (go.name.Equals("Final"))
                {
                    go.SetActive(true);
                }
            }
        }
    }

}
