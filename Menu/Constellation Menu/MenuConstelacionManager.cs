using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuConstelacionManager : MonoBehaviour
{
    GameObject referencia;

    // Start is called before the first frame update
    void Start()
    {
        referencia = GameObject.FindGameObjectWithTag("Referencia");
    }

    // Update is called once per frame
    void Update()
    {
        if(referencia.activeSelf)
        {
            referencia.SetActive(false);
        }
    }
}
