using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{

    public float Health;


    public void TakeDamage(float Amount)
    {
        Health -= Amount;
        if(Health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }


}//CLASS
