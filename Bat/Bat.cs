using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    Vector3 newposition;
    Vector3 direccion;
    
    public float wanderRadius = 1.5f; //Radio en el que se mueve el murciélago
    public float EsperaParaVolverAtacar=1f;
    GameObject jugador;
    Vector3 direccionNormalizada;
    bool girado;
    Rigidbody2D rb;
    public bool PuedePerseguir;
    public enum Estado { Volar = 0, Perseguir = 1 };
    public Estado estado;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        estado = 0;
      
        PuedePerseguir = true;
        CambiarDirección();
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
        
    }

   

    void CambiarDirección()//Asigna una posicion nueva dentro de un circulo
    {
        newposition = transform.position + Random.insideUnitSphere * wanderRadius;
        
        newposition.z = 0;
    }
    void volar()
    {
        //Debug.Log (newposition);
        direccion = newposition - transform.position;

        if (direccion.magnitude < 0.2)//ha llegado a la posicion
        {
            CambiarDirección();
        }
        Girar();
    }
    void Perseguir()
    {
        if (PuedePerseguir==true)
        {
         
            direccion = jugador.transform.position - transform.position;
           
            Girar();

            if (direccion.magnitude < 0.2)//ha llegado al objetivo
            {
               // Debug.Log("atacar");
                StartCoroutine("Wait");
            }
        }
        else
        {
            estado = 0;
            CambiarDirección();
        }
    }
    void Girar()
    {
        direccionNormalizada = direccion.normalized;
        
        transform.position += direccionNormalizada * velocidadMovimiento * Time.deltaTime;
        if (direccionNormalizada.x < 0 && girado == false)
        {
            girar();
            girado = true;
        }
        else
        {
            if (direccionNormalizada.x > 0 && girado == true)
            {
                girar();
                girado = false;
            }

        }
    }
    void Esquivar()//Va a la dirección contraria
    {
        newposition = transform.position * -1;
        newposition.z = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if(Level_Manager.Instance.estadoJuego == Level_Manager.EstadoJuego.InGame)
        {
            forceUpdateEnemy();
            switch (estado)
            {
                case Estado.Volar:
                    volar();
                    break;

                case Estado.Perseguir:
                    Perseguir();

                    break;
            }
        }
    }

    public override void hurtAnim()
    {
        animator.Play("Bat_Hurt");
        StartCoroutine(timeController());
    }

    public override void muerteEntidad()
    {
        DropExp(10);
        Destroy(this.gameObject);
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        
            collision.gameObject.GetComponent<Sejmet>().setDamage(danyo, this.gameObject,collision.gameObject);
            StartCoroutine("Wait"); 
        }
        else
        {
          
            StartCoroutine("Iratras");


        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CambiarDirección();

    }
    IEnumerator timeController()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
    }
    IEnumerator Wait()//Espera para volver a por el juegador
    {
        PuedePerseguir = false;
        yield return new WaitForSeconds(EsperaParaVolverAtacar);
        PuedePerseguir = true;
    }
    IEnumerator Iratras()//Espera para volver a por el juegador
    {
        Esquivar();
        yield return new WaitForSeconds(0.5f);
        CambiarDirección();
    }
}
