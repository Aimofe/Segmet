using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseWithoutSnake : Enemy
{
    bool broken;

    Animator animator;
    //public GameObject life;
    public int Heal;

    float opacidad;
    //public float transicion;
    //public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        //opacidad = 0;
        broken = false;
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("WithSnake", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (broken == false)
        {
            forceUpdateEnemy();

            if (vida <= 0)
            {
                // Debug.Log("se rompio");
                animator.SetBool("Rota", true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                broken = true;
                
            }
        }
        else
        {
            //for (int i = 0; i < Life; i++)
            //{
            //    GameObject heal = Instantiate(life);
            //    heal.transform.position = new Vector2(transform.position.x, transform.position.y);
            //    heal.GetComponent<Life>().life = 1;
            //    Debug.Log("nuemro" + i);
            //}
        }
    }
    public override void muerteEntidad()
    {
        DropLife(Heal);
        StartCoroutine("Wait");
        //Transicion();
    }
    IEnumerator Wait()//Espera para volver a por el juegador
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    //public void Transicion()
    //{
    //    opacidad += velocidad * Time.deltaTime;

    //    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacidad);
    //    if (opacidad==1)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
