using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum TipoEntity {
        Sejmet = 0,
        Escarabajo = 1,
        Bat = 2,
        Serpiente = 3
    };
    public TipoEntity tipo;
    public float vida;
    public float velocidadMovimiento;
    public float danyo;
    public bool faceRight;
    public bool isInvulnerable = false;
    float invulnerableTime = 0f;
    public GameObject deadAnim;
    public GameObject hurtAnimGO;

    //COMPONENTES
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    List<GameObject> listaHijos = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {

    }

    protected void forceUpdate()
    {
        if (invulnerableTime > 0)
        {
            invulnerableTime -= Time.deltaTime;
        }
        else if (invulnerableTime <= 0)
        {
            isInvulnerable = false;
        }
    }

    //METODOS
    protected void girar()//El personaje mira en otra dirección
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        faceRight = !faceRight;
    }

    //public abstract void movimiento(float axisValue);

    //GETTER & SETTERS
    public float getVida()
    {
        return vida;
    }

    public abstract void muerteEntidad();
    public abstract void hurtAnim();

    public void setDamage(float damage, GameObject atacante,GameObject victima)
    {
        if (!isInvulnerable)
        {
            hurtAnimColor(victima);
            if(victima.tag != "CardinalBosses")
            rigidBody = GetComponent<Rigidbody2D>();
            else
                rigidBody = GetComponentInChildren<Rigidbody2D>();
            isInvulnerable = true;
            invulnerableTime = 1f;
            if(transform.position.x > atacante.transform.position.x)
            {
                rigidBody.AddForce(new Vector2(5 * 10f, 0), ForceMode2D.Impulse);
            }
            else
            {
                rigidBody.AddForce(new Vector2(-5 * 10f, 0), ForceMode2D.Impulse);              
            }
            //Sacar esto de aqui y meterlo en cardinal
            switch (victima.name)
            {
                case "North":
                    victima.GetComponentInParent<CardinalBosses>().headHealth[0] -= damage;
                    if (victima.GetComponentInParent<CardinalBosses>().headHealth[0] <= 0)
                        victima.GetComponent<Cabras>().estadoActual = Cabras.EstadoActual.Muerto;
                    break;
                case "West":                 
                    victima.GetComponentInParent<CardinalBosses>().headHealth[1] -= damage;
                    if (victima.GetComponentInParent<CardinalBosses>().headHealth[1] <= 0)
                        victima.GetComponent<Cabras>().estadoActual = Cabras.EstadoActual.Muerto;
                    break;
                case "South":
                    victima.GetComponentInParent<CardinalBosses>().headHealth[2] -= damage;
                    if (victima.GetComponentInParent<CardinalBosses>().headHealth[2] <= 0)
                        victima.GetComponent<Cabras>().estadoActual = Cabras.EstadoActual.Muerto;
                    break;
                case "East":
                    victima.GetComponentInParent<CardinalBosses>().headHealth[3] -= damage;
                    if (victima.GetComponentInParent<CardinalBosses>().headHealth[3] <= 0)
                        victima.GetComponent<Cabras>().estadoActual = Cabras.EstadoActual.Muerto;
                    break;
            }

            if (atacante.tag == "Player")
            {
                //CURA A SEJMET CON CADA TICK DE DAÑO
                if (atacante.GetComponent<Sejmet>().getWaterAtackLevel() == 3)
                {
                    atacante.GetComponent<Sejmet>().takeHeal(atacante.GetComponent<Sejmet>().data.weterCannonHeal);
                }
            }

            hurtAnim();
            if (victima.tag != "CardinalBosses")
                vida -= damage;

            if (vida <= 0)
            {
                GameObject deadAnimGO = Instantiate(deadAnim);
                deadAnimGO.transform.position = transform.position;
                muerteEntidad();
            }
            else
            {
                GameObject deadAnimGO = Instantiate(hurtAnimGO);
                if (tipo == TipoEntity.Escarabajo)
                {
                    deadAnimGO.transform.position = transform.GetChild(1).transform.position;
                }
                else
                { 
                    deadAnimGO.transform.position = transform.position;
                }

            }
        }
    }

    void hurtAnimColor(GameObject victima)
    {
        listaHijos.Clear();
        findChild(gameObject);

        foreach(GameObject child in listaHijos)
        {
            Anima2D.SpriteMeshInstance spriteMeshInstance = child.GetComponent<Anima2D.SpriteMeshInstance>();
            if (spriteMeshInstance != null)
            {
                Anima2D.SpriteMeshInstance sprite = child.GetComponent<Anima2D.SpriteMeshInstance>();
                sprite.color = Color.red;
                StartCoroutine(hurtColor(sprite));
            }
        }
    }

    void findChild(GameObject actualGO)
    {
        for(int i = 0; i < actualGO.transform.childCount; i++)
        {
            GameObject child = actualGO.transform.GetChild(i).gameObject;
            listaHijos.Add(child);

            if(child.transform.childCount != 0)
            {                
                findChild(child);
            }

        }
    }

    IEnumerator hurtColor(Anima2D.SpriteMeshInstance sprite)
    {
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public void push(float force, Vector2 direction)
    {
        StartCoroutine(pushHit(force, direction));
    }

    IEnumerator pushHit(float force, Vector2 direction)
    {
        int valor = 0;
        do
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(direction.x * force, 0));
            valor++;
            yield return new WaitForSeconds(0.05f);
        } while (valor < 3);
        rigidBody.velocity = new Vector2(0, 0);
    }

}
