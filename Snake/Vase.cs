using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Enemy
{
    // Start is called before the first frame update
   public GameObject snake;
   public Animator animator;
   public float wait;
   public bool IsActive;
    bool isOut;
    void Start()
    {
        
        snake.SetActive(false);
        IsActive = false;
        isOut = false;
        animator.SetBool("WithSnake", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (snake!=null)
        {
            forceUpdateEnemy();
            if (IsActive != false)
            {
                snake.GetComponent<Snake>().estado = Snake.Estado.Patrullar;
                StartCoroutine("SacarSnake");
                //gameObject.tag = "Plataforma";
            }
            if (vida <= 0)
            {
                snake.GetComponent<Snake>().estado = Snake.Estado.Perseguir;

                StartCoroutine("SacarSnake");
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //void Animacion()
    //{
    //    animator.SetBool("Rota", true);
    //    snake.SetActive(true);
    //    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //}
    IEnumerator SacarSnake()//Espera para romperse
    {
        animator.SetBool("Rota", true);
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(wait);
        snake.SetActive(true);
       
        //gameObject.GetComponent<Vase>().enabled = false;
    }

}
