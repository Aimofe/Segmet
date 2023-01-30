using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
   //Indica cuanta distancia se va a mover
   public Vector3 movement = new Vector3(0, 1, 0);

   public float velocity;
   public float damage;

   public bool CanMove;
  
    // Start is called before the first frame update
    private void Awake()
    {
        movement += transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove==true)
        {
            transform.position = Vector2.MoveTowards(transform.position, movement, velocity * Time.deltaTime);
        }
    }  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<Sejmet>().setDamage(damage, gameObject, collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Sejmet>().setDamage(damage, gameObject, collision.gameObject);
        }
    }
    
}
