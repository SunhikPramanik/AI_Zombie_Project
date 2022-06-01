using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovementScript : MonoBehaviour
{
    public enum state
    {
        PATROL,
        PURSUE,
        ATTACK,
    }

    #region Variables and References
    private int z_points;
    private Vector3 z_moveDirection;
    private Quaternion z_lookDirection;
    private bool z_isWalking = true;
    private NavMeshAgent z_navMeshAgent;
    private state z_state;
    private float z_playerZombieDistance;
    private Collider[] z_colliderList;
    private float z_overcastSphereRadius;

    [SerializeField]
    private GameObject[] z_waypoints;
    [SerializeField]
    private Animator z_animator;
    [SerializeField]
    private int z_radius;
    [SerializeField]
    private bool z_followPlayer;
    [SerializeField]
    private GameObject player;

    public float z_rotateSpeed;
    public float z_moveSpeed;
    //public Transform player;
    #endregion

    private void Awake()
    {
     if(z_animator == null) 
      { 
        z_animator = GetComponent<Animator>();
      }    
     if(z_navMeshAgent == null)
      {
        z_navMeshAgent = GetComponent<NavMeshAgent>();
      }
    }

    void Start()
    {
        traverse();
    }

    void Update()
    {
        /*z_playerZombieDistance = Vector3.Distance(transform.position, player.transform.position);
        if(z_playerZombieDistance < z_radius)
        {
            z_navMeshAgent.SetDestination(player.transform.position);
            Walk(z_isWalking);
        }*/
        #region Vector3Waypoints
        /*if(Vector3.Distance(z_waypoints[z_points].transform.position, this.transform.position) <= 1)
        if(Vector3.Distance(z_waypoints[z_points].transform.position, this.transform.position) <= 1 && z_followPlayer == false)
        {
            z_points=Random.Range(0, z_waypoints.Length);
            z_isWalking = true;
            if(z_points > z_waypoints.Length)
            {
                z_points = 0;
            }

        }
        z_colliderList = Physics.OverlapSphere(this.transform.position, z_overcastSphereRadius);
        foreach (Collider z_colliders in z_colliderList)
        {
            if (z_colliders.CompareTag("Player"))
            {
                z_followPlayer = true;
                z_playerPosition = z_colliders.transform.position;
                z_NavMeshAgent.SetDestination(z_playerPosition);
                Debug.Log("Player" + z_playerPosition);
            }
        }
        z_moveDirection = z_waypoints[z_points].transform.position;
        z_moveDirection = player.position;
        z_NavMeshAgent.SetDestination(z_moveDirection);
        z_lookDirection = Quaternion.LookRotation(z_moveDirection);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, z_lookDirection, z_rotateSpeed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, z_moveDirection, z_moveSpeed * Time.deltaTime);
        Walk(z_isWalking);*/
        #endregion
        #region NavMeshMovement
        switch (z_state)
        {
            case state.PATROL:
                if (z_navMeshAgent.remainingDistance < 1)
                {
                    traverse();
                }
                break;

        }
        /*if(Vector3.Distance(this.transform.position, playerPosition) <= z_radius)
        {
            z_moveDirection = playerPosition;
            z_navMeshAgent.SetDestination(z_moveDirection);
        }*/
        #endregion
    }

    void Walk(bool value)
    {
        bool z_walkValue;
        z_walkValue= value;
        z_animator.SetBool("Walk", z_walkValue);
    }

    void traverse()
    {
        z_points = Random.Range(0, z_waypoints.Length);
        z_moveDirection = z_waypoints[z_points].transform.position;
        z_navMeshAgent.SetDestination(z_moveDirection);
        Walk(z_isWalking);
    }

}
