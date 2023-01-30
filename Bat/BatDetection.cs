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
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject[] bats = GameObject.FindGameObjectsWithTag("Bat");
            GetComponentInParent<Bat>().estado = Bat.Estado.Perseguir;

            for (int i = 0; i < bats.Length; i++)
            {
                bats[i].GetComponentInParent<Bat>().estado = Bat.Estado.Perseguir;
            }
        }
        
    }
}
