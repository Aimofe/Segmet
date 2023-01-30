using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public enum TipoProyectil { Plant = 0, Escorpion = 1}
    TipoProyectil tipoProyectil;
    GameObject target;
    float finalSubida;
    float startFollow;
    Rigidbody2D rb;
    float maxTimeLive;
    public float damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        finalSubida = Time.realtimeSinceStartup + 0.5f;
        startFollow = finalSubida + 0.2f;
        target = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        maxTimeLive = Time.realtimeSinceStartup + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (tipoProyectil)
        {
            case TipoProyectil.Plant:

                if(maxTimeLive <= Time.realtimeSinceStartup)
                {
                    Destroy(gameObject);
                }

                if(startFollow <= Time.realtimeSinceStartup)
                {
                    //ATAQUE
                    followCharacter();
                }
                else if(finalSubida <= Time.realtimeSinceStartup)
                {
                    //CAIDA
                    caida();
                }
                else
                {
                    //SUBIDA
                    plantSubida();
                }
                break;
        }
    }

    void caida()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void plantSubida()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), 7f * Time.deltaTime);
    }

    void followCharacter()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 7f * Time.deltaTime);
        transform.up = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (tipoProyectil)
            {
                case TipoProyectil.Plant:
                    plantHit(collision);
                    break;
            }
        }
        else if(collision.tag == "Suelo" || collision.tag == "Plataforma")
        {
            Destroy(gameObject);
        }

    }

    void plantHit(Collider2D hit)
    {
        hit.GetComponent<Sejmet>().setDamage(damage, gameObject,hit.gameObject);
        Destroy(gameObject);
    }

}
