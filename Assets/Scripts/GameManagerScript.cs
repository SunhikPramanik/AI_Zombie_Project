using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie_Prefab;
    [SerializeField]
    private GameObject[] z_waypoints;
    [SerializeField]
    private int size;
   
    public GameObject player;

    private int points;

    public static GameManagerScript instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

       for(int i=0; i< size; i++)
        {
            points = Random.Range(0, z_waypoints.Length);
            Instantiate(zombie_Prefab, z_waypoints[points].transform.position, Quaternion.identity);
        }

        Debug.Log("name 1 "+player.name);
    }

    void Update()
    {
       
    }

   
}
