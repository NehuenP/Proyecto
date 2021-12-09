using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public Rigidbody rb;

    [Range(1, 10)]
    public float velocidad = 5;
    public float sensitivity;
    private string[] misiones = { "Estaciones", "Emociones" };
    private bool completado = true;
    private float cTime=0;
    private bool movimiento;
    // Start is called before the first frame update
    void Start()
    {
        movimiento = true;
        foreach(string s in misiones)
        {
            GameObject.Find("Toggle_" + s).GetComponent<Toggle>().isOn= PlayerPrefs.GetInt(s)==1;
            if(completado != false)
            {
                completado = PlayerPrefs.GetInt(s) == 1;
            }
        }
        if (completado)
        {
            victoria();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movimiento)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.GetComponent<Transform>().Translate(Vector3.right * Time.deltaTime * 3);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.GetComponent<Transform>().Translate(Vector3.left * Time.deltaTime * 3);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.GetComponent<Transform>().Translate(Vector3.forward * Time.deltaTime * 3);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.GetComponent<Transform>().Translate(Vector3.back * Time.deltaTime * 3);
            }
        }
        
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetMouseButton(1) && Time.timeScale == 1)
            {
                float rotateHorizontal = Input.GetAxis("Mouse X");
                transform.RotateAround(this.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
                if (!movimiento)
                {
                    GameObject.Find("Advertencia").SetActive(false);
                }
                movimiento = true;

            }
            
        }
        if (cTime > 0)
        {
            cTime -= Time.deltaTime * 2;
        }
        if (cTime < 0)
        {
            GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
            if (fooGroup.Length > 0)
            {
                foreach (GameObject go in fooGroup)
                {
                    if (go.name.Equals("Victoria"))
                    {
                        go.SetActive(true);
                    }
                }
            }
        }
    }

    public void victoria()
    {
        cTime = 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        if (fooGroup.Length > 0)
        {
            foreach (GameObject go in fooGroup)
            {
                if (go.name.Equals("Advertencia"))
                {
                    go.SetActive(true);
                }
            }
        }
        movimiento = false;
    }
}
