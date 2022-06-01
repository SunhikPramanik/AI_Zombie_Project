using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementScript : MonoBehaviour
{
    public Camera cam;
    public Animator animator;
    NavMeshAgent navMeshAgent;
    bool run;
    
    [SerializeField]
    private float p_moveSpeed;


    public static PlayerMovementScript instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            run = true;
        }
        else
        {
            run = false;
        }
        animator.SetBool("Run", run);
    }
}
