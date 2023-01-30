using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject snake;
    void Start()
    {
        snake = GetComponentInParent<Vase>().snake;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //snake.SetActive(true);
            GetComponentInParent<Vase>().IsActive =  true;
            //gameObject.GetComponentInChildren<Snake>().estado = Snake.Estado.Patrullar;
            //snake.GetComponent<Snake>().estado = Snake.Estado.Patrullar;
            
        }
    }
   
}
