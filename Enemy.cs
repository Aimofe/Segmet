using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //protected float exp;
    protected GameObject[] drop;
    public GameObject experience;
    public GameObject life;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void forceUpdateEnemy()
    {
        forceUpdate();
    }

    public void movimiento(float axisValue)
    {
    }

    public override void muerteEntidad()
    {

    }

    public override void hurtAnim()
    {
    }

    public void FogStunning()
    {
        print("FogStunning");
    }

    public IEnumerator RecoverOldLinearDrag(float number)
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().drag = number;
    }

    public void DropExp(int exp)
    {

        for(int i = 0; i < exp; i++)
        {
            GameObject exper = Instantiate(experience);
            exper.transform.position = new Vector2(transform.position.x, transform.position.y);
            exper.GetComponent<Exp>().exp = 1;
        }
        
    }
    public void DropLife(int Life)
    {
        for (int i = 0; i < Life; i++)
        {
            GameObject heal = Instantiate(life);
            heal.transform.position = new Vector2(transform.position.x, transform.position.y);
            heal.GetComponent<Life>().life = 1;
        }
    }
}
