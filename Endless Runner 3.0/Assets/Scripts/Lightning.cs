using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject Particle_Lightning;
    public AudioClip PickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")// lets check if this is player or not
        {
            Instantiate(Particle_Lightning, transform.position, Quaternion.identity);// instantiate effect
            LevelManager.instance.Lightning();
            Destroy(gameObject);//destroy game objet Coin
            LevelManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PickupSound, 1);//play on shot
        }
    }
}
