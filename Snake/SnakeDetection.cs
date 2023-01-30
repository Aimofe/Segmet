using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDetection : MonoBehaviour
{
    bool SeeTrap;
    // Start is called before the first frame update
    void Start()
    {
        SeeTrap = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            if (SeeTrap == true)
            {
                GetComponentInParent<Snake>().estado = Snake.Estado.Patrullar;
            }
            else
            {
                //Debug.Log("detecto");
                //GetComponent<Snake>().estado = Snake.Estado.Perseguir;
                GetComponentInParent<Snake>().estado = Snake.Estado.Perseguir;
               

            }
        }
        else if (collision.tag == "Trap")
        {
            SeeTrap = true;

        }
        
    }
   
}
