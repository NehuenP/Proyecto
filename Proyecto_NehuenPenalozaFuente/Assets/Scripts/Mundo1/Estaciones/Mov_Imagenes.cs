using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mov_Imagenes : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float x;
    private float y;
    private bool movimiento = false;
    private bool acierto = false;
    public int imagenes;
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        if (acierto == false)
        {
            if(Time.timeScale == 1)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
                movimiento = true;
            }
            
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && movimiento)
        {
            //Debug.Log("x = "+ transform.position.x+" y = "+transform.position.y);
            switch (this.name)
            {
                case "Gorro":
                    checkPosition(new int[] { 200, 260, 291, 478 }, new string[] { "invierno" }, new int[] { 405, 245 });
                    break;
                case "Chanclas":
                    checkPosition(new int[] { 20, 53, 350, 458 }, new string[] { "verano" }, new int[] { 405, 32 });
                    break;
                case "Botas":
                    checkPosition(new int[] { 20, 53, 350, 458 }, new string[] { "otonio","invierno" }, new int[] { 405, 32 });
                    break;
                case "Zapatillas":
                    checkPosition(new int[] { 20, 53, 350, 458 }, new string[] {"primavera" }, new int[] { 405, 32 });
                    break;
                case "Abrigo":
                    checkPosition(new int[] { 110, 155, 375, 450 }, new string[] { "invierno" }, new int[] { 405, 126 });
                    break;
                case "Guantes":
                    float xTemp = gameObject.transform.position.x;
                    float yTemp = gameObject.transform.position.y;
                    checkPosition(new int[] { 85, 105, 350, 396 }, new string[] { "invierno" }, new int[] { 358, 98 });
                    if (!acierto)
                    {
                        gameObject.transform.position = new Vector3(xTemp, yTemp, transform.position.z);
                        checkPosition(new int[] { 85, 105, 417, 468 }, new string[] { "invierno" }, new int[] { 358, 98 });
                    }
                    break;
                case "Chubasquero":
                    checkPosition(new int[] { 97, 149, 373, 450 }, new string[] { "otonio" }, new int[] { 402, 125 });
                    break;
                case "Pantalon":
                    checkPosition(new int[] { 33, 99, 361, 451 }, new string[] { "otonio", "invierno" }, new int[] { 407, 73 });
                    break;
                case "Camiseta":
                    checkPosition(new int[] { 101, 151, 367, 463 }, new string[] { "verano", "primavera" }, new int[] { 407, 127 });
                    break;
                case "Cortos":
                    checkPosition(new int[] { 56, 106, 362, 460 }, new string[] { "verano", "primavera" }, new int[] { 407, 90 });
                    break;
                case "Gafas de Sol":
                    checkPosition(new int[] { 162, 205, 363, 473 }, new string[] { "verano"}, new int[] { 409, 189 });
                    break;
            }
            

        }
    }

    private void checkPosition(int[] coord,string[] estaciones, int[] coordFin)
    {
        if ((gameObject.transform.position.y <= coord[0] || gameObject.transform.position.y >= coord[1]) || (gameObject.transform.position.x <= coord[2] || gameObject.transform.position.x >= coord[3]))
        {
            gameObject.transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            for(int i=0; i<estaciones.Length && !acierto; i++)
            {
                if (GameObject.Find(estaciones[i]))
                {
                    gameObject.SetActive(false);
                    GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
                    if (fooGroup.Length > 0)
                    {
                        foreach (GameObject go in fooGroup)
                        {
                            if(go.name.Contains(gameObject.name + "_Final"))
                             {
                                go.SetActive(true);
                             }
                         }
                     }
                    PlayerPrefs.SetInt("aciertos", PlayerPrefs.GetInt("aciertos") + 1);
                    acierto = true;
                }
            }
            
            if(!acierto)
            {
                gameObject.transform.position = new Vector3(x, y, transform.position.z);
            }

        }
    }
}
