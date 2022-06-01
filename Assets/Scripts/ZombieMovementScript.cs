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
    private Color z_gizmosColor;
    private Collider[] z_colliderList;
    private float z_overcastSphereRadius;

    [SerializeField]
    private GameObject[] z_waypoints;
    [SerializeField]
    private Animator z_animator;
    [SerializeField]
    private int z_radius;
    [SerializeField]
    private bool z_followPlayer;    //[SerializeField]
   

    public float z_rotateSpeed;
    public float z_moveSpeed;
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
       // player = GameManagerScript.instance.player;
    }

    void Start()
    {
        z_state = state.PATROL;
        Debug.Log("name 2" + GameManagerScript.instance.player.name);
    }

    void Update()
    {
        z_playerZombieDistance = Vector3.Distance(transform.position, GameManagerScript.instance.player.transform.position);
        if (z_playerZombieDistance < z_radius)
        {
            z_state = state.PURSUE;
        }
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

            case state.PURSUE:
                pursue();
                break;

            case state.ATTACK:
                break;

        }
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
        z_gizmosColor = Color.black;
        if (z_playerZombieDistance > z_radius)
        {
            z_points = Random.Range(0, z_waypoints.Length);
            z_moveDirection = z_waypoints[z_points].transform.position;
            z_navMeshAgent.SetDestination(z_moveDirection);
            Walk(z_isWalking);
            z_state = state.PATROL;
        }
        if (z_playerZombieDistance < z_radius)
        {
            z_state = state.PURSUE;
        }
    }

    void pursue()
    {
        z_gizmosColor = Color.red;
        z_navMeshAgent.SetDestination(GameManagerScript.instance.player.transform.position);
        Walk(z_isWalking);
        if (z_playerZombieDistance > z_radius)
        {
            z_state = state.PATROL;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= z_gizmosColor;
        Gizmos.DrawWireSphere(this.transform.position, z_radius);
    }
}
