using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Emociones : MonoBehaviour
{
    public List<Sprite> ejemplos;
    public List<Sprite> emociones;
    private List<int> usadas;
    private int i;//ejemplo
    private int j;//emocion
    private int aciertos;
    private int errores;
    private Image ejemplo;
    private Image emocion;
    private float cTime;//check
    private float cTime2;//movimiento
    private float cTime3;//intervalo en el que sale el mensaje
    private float cTime4;//tiempo que dura el mensaje
    private GameObject animacionCheck;
    private GameObject animacionEmocion;
    private string dificultad;
    private string[] frasesAnimo = new string[] { "¡Solo queda una!", "¡Tú puedes!", "¡Ánimo!", "¡Vas muy bien!", "¡Sigue así!", "¡Adelante!", "¡Genial!" };
    public Text textoAnimo;
    private bool animando;

    void Start()
    {
        usadas = new List<int>();
        cambioImagen();
        animacionCheck = GameObject.Find("Check");
        animacionEmocion = GameObject.Find("Animacion");
        dificultad = PlayerPrefs.GetString("dificultad");
        if (dificultad == "Dificil")
            GameObject.Find("Definicion").SetActive(false);
        else
            GameObject.Find("Errores").SetActive(false);
        animando = false;
        cTime3 = 31;
        cTime4 = 0;
    }


    void Update()
    {
        if (cTime > 0)
        {
            cTime -= Time.deltaTime*15;
        }
        if (cTime < 0)
        {
            animacionCheck.GetComponent<Animator>().SetInteger("estado", 0);
        }

        if (cTime2 > 0)
        {
            cTime2 -= Time.deltaTime * 20;
        }
        if (cTime2 < 0)
        {
            animacionEmocion.GetComponent<Animator>().SetInteger("lado", 0);
            animacionEmocion.GetComponent<Animator>().SetInteger("imagen", -1);
            emocion.gameObject.SetActive(true);
        }
        if (cTime4 > 0)
        {
            cTime4 -= Time.deltaTime * 2;
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
        if (cTime3 > 0)
        {
            cTime3 -= Time.deltaTime * 2;
        }
        else
        {
            cTime3 = 31;
            cTime4 = 6;
            Animar(frasesAnimo[Random.Range(1, frasesAnimo.Length)]);
        }
    }
     private void cambioImagen()
    {
        do
        {
            i = Random.Range(0, ejemplos.Count);
        }
        while (usadas.Contains(i));
        ejemplo = GameObject.Find("Ejemplo").GetComponent<Image>();
        ejemplo.sprite=ejemplos[i];
        if(dificultad!="Dificil")
            GameObject.Find("Definicion").GetComponent<Text>().text = ejemplo.sprite.name;
        j = Random.Range(0, emociones.Count);
        emocion = GameObject.Find("ImagenEleccion").GetComponent<Image>();
        emocion.sprite = emociones[j];
    }

    public void comprobar()
    {
        if (i == j)
        {
            usadas.Add(i);
            cTime = 29;
            animacionCheck.GetComponent<Animator>().SetInteger("estado", 1);
            aciertos++;
            GameObject.Find("Aciertos").GetComponent<Text>().text = "Aciertos: " + aciertos;
            if(usadas.Count<emociones.Count)
                cambioImagen();
            
        }
        else
        {
            cTime = 41;
            if (dificultad == "Dificil")
            {
                animacionCheck.GetComponent<Animator>().SetInteger("estado", -1);
                errores++;
                GameObject.Find("Errores").GetComponent<Text>().text = "Errores: " + errores;
            }
        }
        switch (dificultad)
        {
            case "Facil":
                if (aciertos == 3)
                {
                    victoria();
                }
                if (aciertos == 2){
                    cTime3 = 31;
                    cTime4 = 6;
                    Animar(frasesAnimo[0]);
                }
                break;
            case "Medio":
                if (aciertos == emociones.Count)
                {
                    victoria();
                }
                if (aciertos == emociones.Count-1)
                {
                    cTime3 = 31;
                    cTime4 = 6;
                    Animar(frasesAnimo[0]);
                }
                break;
            case "Dificil":
                if (aciertos == emociones.Count)
                {
                    victoria();
                }
                if (aciertos == emociones.Count - 1)
                {
                    cTime3 = 31;
                    cTime4 = 6;
                    Animar(frasesAnimo[0]);
                }
                else if(errores == 3)
                {
                    SceneManager.LoadScene("Emociones");
                }
                break;
        }
    }

    public void cambioPosicion(int cambio)
    {
        cambioAnimacion(cambio, j);
        j += cambio;
        if (j == emociones.Count)
            j = 0;
        else if (j < 0)
            j = emociones.Count - 1;
        emocion.sprite = emociones[j];
    }

    private void cambioAnimacion(int lado, int imagen)
    {
        animacionEmocion.GetComponent<Animator>().SetInteger("lado", lado);
        animacionEmocion.GetComponent<Animator>().SetInteger("imagen", imagen);
        emocion.gameObject.SetActive(false);
        cTime2 = 61;
    }
    private void victoria()
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
    
}
