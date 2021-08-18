using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private float Health;

    private EnemyMovement EnemyMovement;
    private NavMeshAgent agent;
    private Animator anim;
    private CharacterController characterController;

    private void OnEnable()
    {
        EnemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
    }

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
        characterController.enabled = false;

        anim.enabled = false;
        agent.enabled = false;
        EnemyMovement.enabled = false;
        Destroy(gameObject, 5f);
        
        this.enabled = false;
    }


}//CLASS
