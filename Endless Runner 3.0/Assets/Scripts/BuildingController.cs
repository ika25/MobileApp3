using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private bool hasAppeared = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() // in here we need to calculate distance between the gameObject and player
    {
        if (Vector3.Distance(transform.position, player.position) < 200 && !hasAppeared) ;
        {
            animator.SetTrigger("Appear");
            hasAppeared = true;
        }
    }
}
