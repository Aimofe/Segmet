using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    public enum Estado { Patrullar = 0, Perseguir = 1, InVase = 2, };
    public Estado estado;

    GameObject jugador;
    GameObject SnakeGrounded;
    GameObject SnakeDetection;
    Transform groundDetect;
    public float plataformdistant;//distancia que necesita el grondDetect para detectar el suelo
    float atackTime = 1f;
    float nextAttack;
    public float EsperaParaVolverAtacar = 2f;

    Rigidbody2D rb;

    Vector3 direccion;
    Vector3 direccionNormalizada;
   
    public Animator animator { get; set; }
    
    public bool canAttack { get; set;}
    public bool persiguiendo { get; set; }
    bool moveLeft;

    void Start()
    {
        estado = 0;
        jugador = GameObject.FindGameObjectWithTag("Player");
        
        
        SnakeGrounded = GameObject.FindGameObjectWithTag("SnakeGrounded");
        SnakeDetection = GameObject.FindGameObjectWithTag("SnakeDetection");
        SnakeDetection.SetActive(true);
        SnakeGrounded.SetActive(true);
        groundDetect = SnakeGrounded.transform;

        rb = this.GetComponent<Rigidbody2D>();

        GameObject child = gameObject.transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
        
        canAttack = true;
        moveLeft = true;
    }
    void Update()
    {
        if(Level_Manager.Instance.estadoJuego == Level_Manager.EstadoJuego.InGame)
        {
            forceUpdateEnemy();
            switch (estado)
            {
                case Estado.Patrullar:
                    Patrulla();
                    break;

                case Estado.Perseguir:
                    Perseguir(jugador);
                    break;
            }
        }
    }
    public override void muerteEntidad()
    {
        DropExp(10);
        Destroy(this.gameObject);
    }
    void Patrulla()
    {
        persiguiendo = false;
        //if (onGround == true)
        //{
        //    Debug.Log("onground");
        //    direccionNormalizada = direccion.normalized;
        //    direccionNormalizada.y = 0;
        //}
        if (moveLeft == true)
        {
            direccion = Vector2.left;
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
        }
        else
        {
            direccion = Vector2.right;
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
        }
        
        RaycastHit2D ground = Physics2D.Raycast(groundDetect.position, Vector2.down, plataformdistant);
        //RaycastHit2D wall = Physics2D.Raycast(groundDetect.position, Vector2.left, plataformdistant);
       
         if (ground.collider == false)
         {
            CambiarDireccion(persiguiendo);
         }
            /*
            else if (wall.collider==true&&onGround==true)
            {
                CambiarDireccion();
            }*/

    }
    void Perseguir(GameObject target)
    {
        persiguiendo = true;
        direccion = target.transform.position - transform.position;
        Debug.Log("estapersiguiendp");
        //transform.position = transform.position + (direccion.normalized * velocidadMovimiento) * Time.deltaTime;
        if (canAttack==true)
        {
            transform.Translate(direccion.normalized * velocidadMovimiento * Time.deltaTime);
            CambiarDireccion(persiguiendo);
        }
        else
        {
            Debug.Log("nopuedeatacar");
            transform.Translate(-direccion.normalized * velocidadMovimiento * Time.deltaTime);
            CambiarDireccion(persiguiendo);
            StartCoroutine(Wait());
        }
           
        
    }
   
    public void CambiarDireccion(bool perseguir)
    {
        if (direccion.normalized.x < 0 && moveLeft == !perseguir)
        {
            girar();
            moveLeft = perseguir;
        }
        else
        {
            if (direccion.normalized.x > 0 && moveLeft == perseguir)
            {
                girar();
                moveLeft = !perseguir;
            }
        }
    }
   
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            if (Time.realtimeSinceStartup >= nextAttack)
            {
                nextAttack = Time.realtimeSinceStartup + atackTime;
                animator.Play("SnakeAttack");
                collision.gameObject.GetComponent<Sejmet>().setDamage(GetComponentInParent<Snake>().danyo, gameObject,collision.gameObject);
                canAttack = false;
            }

        }
        else
        {
            if (collision.gameObject.tag != "Suelo" || collision.gameObject.tag != "Player")
            {
                CambiarDireccion(persiguiendo);
            }

        }
    }
    IEnumerator Wait()//Espera para volver a por el juegador
    {
        yield return new WaitForSeconds(EsperaParaVolverAtacar);
        canAttack = true;
    }
}
