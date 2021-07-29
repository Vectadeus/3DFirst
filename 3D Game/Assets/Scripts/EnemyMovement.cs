using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent Agent;
    public Transform player;
    

    //STATES

    public float SightRange, AttackRange;
    public bool InSightRange, InAttackRange;



    Collider[] colliders;
    public float PlayersFindRadius;
    public LayerMask PlayerMask;


    //ToFindNearest
    public Collider Nearest;


    // Animations
    Animator anim;
    bool IsMoving;




    private void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindPlayerQoroutine());
        StartCoroutine(CheckMovement());
            
    }

    // Update is called once per frame
    void Update()
    {

        PlayAnimations();
        EnemyActions();

    }





    IEnumerator FindPlayerQoroutine()
    {
        FindPlayer();
        yield return new WaitForSeconds(1f);
        StartCoroutine(FindPlayerQoroutine());
    }





    void FindPlayer()
    {
        colliders = Physics.OverlapSphere(transform.position, PlayersFindRadius, PlayerMask);
        foreach(Collider collider in colliders)
        {

            if (Nearest == null)
            {
                Nearest = collider;
            }
            if (Nearest != null && Vector3.Distance(transform.position, collider.transform.position) < Vector3.Distance(transform.position, Nearest.transform.position))
            {
                Nearest = collider;
            }

        }

    }

   IEnumerator CheckMovement()
    {
        Vector3 prevpos = transform.position;
        yield return new WaitForSeconds(0.05f);
        if(prevpos.x == transform.position.x && prevpos.z == transform.position.z)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }
        StartCoroutine(CheckMovement());
    }
    

    void PlayAnimations()
    {
        if (IsMoving)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
    void EnemyActionss()
    {
        if(Vector3.Distance(transform.position , Nearest.transform.position) <= SightRange)
        {
            //Gaekite
        }
        if(Vector3.Distance(transform.position , Nearest.transform.position) <= AttackRange)
        {
            //ATTACK
        }
        Agent.SetDestination(Nearest.transform.position);
    }







    void EnemyActions()
    {
        if(Nearest != null)
        {
        if(Vector3.Distance(transform.position , Nearest.transform.position) <= SightRange)
        {
            Agent.SetDestination(Nearest.transform.position);
        }

        if(Vector3.Distance(transform.position , Nearest.transform.position) <= AttackRange)
        {
            Agent.SetDestination(transform.position);
        }

        }
    }












    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PlayersFindRadius);
    }







}//CLASS
