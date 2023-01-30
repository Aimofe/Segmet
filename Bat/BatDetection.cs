using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDetection : MonoBehaviour
{
    GameObject jugador;
    bool perseguir;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //transform.position = jugador.transform.position;
            //perseguir = true;
            //Bat detec = GetComponent<Bat>();
            //detec.estado = Bat.Estado.Perseguir;
            GameObject[] bats = GameObject.FindGameObjectsWithTag("Bat");
            GetComponentInParent<Bat>().estado = Bat.Estado.Perseguir;

            for (int i = 0; i < bats.Length; i++)
            {
                bats[i].GetComponentInParent<Bat>().estado = Bat.Estado.Perseguir;
            }
        }
        //else if (collision.tag=="Bat")
        //{
        //    collision.gameObject.GetComponent<Bat>().estado= Bat.Estado.Perseguir;
        //}
    }
}
