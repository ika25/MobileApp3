using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject Particle_Heart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")// lets check if this is player or not
        {
            Instantiate(Particle_Heart, transform.position, Quaternion.identity);// instantiate effect
            Destroy(gameObject);//destroy game objet heart
            LevelManager.instance.heartCount++;
        }
    }
}