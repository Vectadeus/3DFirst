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

    private void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(player.transform.position);
        FindPlayer();
    }
    









    void FindPlayer()
    {
        colliders = Physics.OverlapSphere(transform.position, PlayersFindRadius, PlayerMask);

        foreach(Collider collider in colliders)
        {
            if(Nearest = null)
            {
            Nearest = collider;
                Debug.Log("HIU");
            }
            if(Nearest != null && Vector3.Distance(transform.position , collider.transform.position) < Vector3.Distance(transform.position, Nearest.transform.position))
            {
                Nearest = collider;
                Debug.Log(collider.name);
            }

            
        }
    }

    void EnemyActions()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PlayersFindRadius);
    }







}//CLASS
