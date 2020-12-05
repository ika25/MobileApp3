using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject newStreet;//prefab we created
    public GameObject currentStreet;//

    private GameObject oldStreet;

    void OnTriggerEnter(Collider other)
    {
        //here we need to check if player has entered start or middle 
        if (other.tag == "Start")
        {
            if (oldStreet != null)
            {
                Destroy(oldStreet);//destroy old street
            }
            else
            {
                Destroy(GameObject.Find("MainScene"));//spawn new level
            }
        }
        else if (other.tag == "Middle")
        {
            SpawnLevel();
        }
    }
    void SpawnLevel()
    {
        oldStreet = currentStreet;//assign street
        currentStreet = (GameObject)Instantiate(newStreet, currentStreet.transform.GetChild(9).position, Quaternion.identity);

    }
}
