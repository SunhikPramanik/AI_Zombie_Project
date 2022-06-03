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
    private bool z_isWalking;
    private bool z_isRunning;
    private bool z_isAttacking;
    private NavMeshAgent z_navMeshAgent;
    private state z_state;
    private float z_playerZombieDistance;
    private Color z_gizmosColor;
    private Collider z_onCollisionWithPlayer;

    [SerializeField]
    private GameObject[] z_waypoints;
    [SerializeField]
    private Animator z_animator;
    [SerializeField]
    private int z_radius;
    [SerializeField]
    private bool z_followPlayer;
   

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
     if(z_onCollisionWithPlayer==null)
        {
            z_onCollisionWithPlayer = GetComponent<BoxCollider>();
        }
    }

    void Start()
    {
        z_isWalking=true;
        z_isRunning = false;
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
        z_animator.SetBool("Walk", value);
    }

    void Run(bool value)
    {
        z_animator.SetBool("Run", value);
    }

    void Attack(bool value)
    {
        z_animator.SetBool("Attack", value);
    }

    void Death()
    {
        z_animator.SetTrigger("Death");
    }

    void traverse()
    {
        z_gizmosColor = Color.black;
        if (z_playerZombieDistance > z_radius)
        {
            z_points = Random.Range(0, z_waypoints.Length);
            z_moveDirection = z_waypoints[z_points].transform.position;
            z_navMeshAgent.SetDestination(z_moveDirection);
            z_navMeshAgent.speed = 3.5f;
            z_isWalking = true;
            Walk(z_isWalking);
            z_isRunning = false;
            Run(z_isRunning);
            z_isAttacking = false;
            Attack(z_isAttacking);
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
        z_navMeshAgent.speed = 7f;
        z_isWalking = false;
        Walk(z_isWalking);
        z_isRunning = true;
        Run(z_isRunning);
        z_isAttacking = false;
        Attack(z_isAttacking);
        if (z_playerZombieDistance > z_radius)
        {
            z_state = state.PATROL;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            z_isWalking = false;
            Walk(z_isWalking);
            z_isRunning = false;
            Run(z_isRunning);
            z_isAttacking = true;
            Attack(z_isAttacking);
            if (z_playerZombieDistance < z_radius)
            {
                z_state = state.PURSUE;
            }
            else if (z_playerZombieDistance > z_radius)
            {
                z_state = state.PATROL;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= z_gizmosColor;
        Gizmos.DrawWireSphere(this.transform.position, z_radius);
    }
}
