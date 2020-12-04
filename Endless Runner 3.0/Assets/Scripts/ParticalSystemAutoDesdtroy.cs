using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemAutoDesdtroy : MonoBehaviour
{
    private ParticleSystem ps;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()//check if partical system is complited
    {
        if (ps.isPlaying)// its not complited
            return;
        Destroy(gameObject);//if its not playing destroy PS
    }
}
