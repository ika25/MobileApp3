using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject Particle_Coin;
    public AudioClip PickupSound;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update postion of the coin
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")// lets check if this is player or not
        {
            Instantiate(Particle_Coin, transform.position, Quaternion.identity);// instantiate effect
            Destroy(gameObject);//destroy game objet Coin
            LevelManager.instance.coinCount++;
            LevelManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PickupSound, 1);//play on shot
        }
    }
}
