using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    //check function that will be called when the player has entered the collider of this object
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(GameObject.Find("Main Camera").gameObject);// removed the main camera
        }
    }
}
