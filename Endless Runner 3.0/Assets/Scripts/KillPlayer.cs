using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision other)//kill the player ones colider has entered player
    {
        if (other.collider.tag == "Player")// check if other 
        {
            if (!other.collider.GetComponent<PlayerController>().IsDead) //check if player is not dead
            {
                other.collider.GetComponent<PlayerController>().KillPlayer();// then kill the player
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
