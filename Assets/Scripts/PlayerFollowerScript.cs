using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerScript : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - GameManagerScript.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameManagerScript.instance.player.transform.position - offset;
    }
}
