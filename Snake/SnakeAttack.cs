using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : MonoBehaviour
{
    float atackTime = 1f;
    float nextAttack;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GetComponentInParent<Snake>().canAttack == true)
        {
            if(Time.realtimeSinceStartup >= nextAttack)
            {
                nextAttack = Time.realtimeSinceStartup + atackTime;
                GetComponentInParent<Snake>().animator.Play("SnakeAttack");
                collision.gameObject.GetComponent<Sejmet>().setDamage(GetComponentInParent<Snake>().danyo, gameObject,collision.gameObject);
            }

        }
        else
        {
            if(collision.tag != "Suelo")
            {
                GetComponentInParent<Snake>().CambiarDireccion(GetComponentInParent<Snake>().persiguiendo);
            }
            
        }
    }
}
