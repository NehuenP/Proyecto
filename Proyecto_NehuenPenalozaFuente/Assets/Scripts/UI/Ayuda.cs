using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayuda : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
            if (fooGroup.Length > 0)
            {
                foreach (GameObject go in fooGroup)
                {
                    if (go.name.Equals("PanelAyuda"))
                    {
                        if (go.activeInHierarchy)
                        {
                            go.SetActive(false);
                            Time.timeScale = 1;
                        }
                        else
                        {
                            go.SetActive(true);
                            Time.timeScale = 0;
                        }
                        
                    }
                }
            }
        }
    }

    public void cerrarAyuda()
    {
        GameObject[] fooGroup = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in fooGroup)
        {
            if (go.name.Equals("PanelAyuda"))
            {
                if (go.activeInHierarchy)
                {
                    GameObject.Find("PanelAyuda").SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
        
    }


}
