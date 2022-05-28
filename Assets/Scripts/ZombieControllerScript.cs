using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControllerScript : MonoBehaviour
{

    public enum state
    {
        IDLE,
        FOLLOWING,
        ATTACK,
    }

    private Vector3 z_originalPosition;

    private Collider[] z_colliderList;
    [SerializeField]
    private NavMeshAgent z_navMeshAgent;
    [SerializeField]
    private float z_overlapSphereRadius;
    [SerializeField]
    private float z_moveSpeed;
    [SerializeField]
    private Vector3 playerPosition;
    [SerializeField]
    private float attackRadius;

    public static ZombieControllerScript instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        z_originalPosition = this.transform.position;
    }

    void Update()
    {
        z_colliderList = Physics.OverlapSphere(this.transform.position, z_overlapSphereRadius);
        foreach(Collider colliders in z_colliderList)
        {
            if (colliders.CompareTag("Player"))
            {
                z_navMeshAgent.SetDestination(colliders.transform.position);Debug.Log(colliders.transform.position);
            }
            else Vector3.MoveTowards(this.transform.position,z_originalPosition, z_moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, z_overlapSphereRadius);
    }
}
