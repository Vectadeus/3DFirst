﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public EnemyMovement EnemyMovement;
    public NavMeshAgent agent;
    public Animator anim;
    public CharacterController characterController;
    public float Health;

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
