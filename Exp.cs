using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    enum EstadoExp { inicio = 0, persecucion = 1}
    EstadoExp estadoExp;
    Rigidbody2D rb;
    GameObject sejmet;
    [HideInInspector]
    public int exp;
    float nextEstado;
    float randomAngle;
    float randomX;
    float randomY; 
    // Start is called before the first frame update
    void Start()
    {
        sejmet = GameObject.FindGameObjectWithTag("Player");
        estadoExp = 0;
        nextEstado = Time.realtimeSinceStartup + 0.1f;
        randomX = Random.Range(-1, 1);
        randomY = Random.Range(-1, 1);
        randomAngle = Random.Range(0, 361);
    }

    // Update is called once per frame
    void Update()
    {

        switch (estadoExp)
        {
            case EstadoExp.inicio:
                inicioExp();
                break;

            case EstadoExp.persecucion:
                followExp();
                break;
        }


    }

    void inicioExp()
    {
        //Vector2 moveTo = new Vector2(randomX, randomY);
        //rb.AddForce(moveTo);
        //transform.Translate(new Vector2(transform.position.x + randomX, transform.position.y + randomY));
        transform.Translate(VectorFromAngle(randomAngle) * Time.deltaTime * 30f);
        if(Time.realtimeSinceStartup >= nextEstado)
        {
            estadoExp = EstadoExp.persecucion;
        }
    }


    Vector2 VectorFromAngle(float angulo)
    {
        return new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
    }

    void followExp()
    {
        transform.position = Vector3.MoveTowards(transform.position, sejmet.transform.position, 30f * Time.deltaTime);
        transform.up = new Vector3(sejmet.transform.position.x, sejmet.transform.position.y, sejmet.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Level_Manager.Instance.exp += exp;
            Destroy(this.gameObject);
        }
    }
}
