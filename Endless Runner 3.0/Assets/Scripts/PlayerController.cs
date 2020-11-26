using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class MovementSettings
    {
        public float forwardVelocity = 10;
        public float jumpVelocity = 20;
        public int forwardInput = 1;
    }

    [System.Serializable]
    public class PhysicsSettings
    {
        public float downAcceleration = 0.75f;
    }

    public MovementSettings movementSettings = new MovementSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();


    private Vector3 velocity;
    private Rigidbody rigidbody;
    private Animator animator;
    private int jumpInput = 0;
    private bool onGround = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputHandling();
        Run();
        CheckGround();
        Jump();
        rigidbody.velocity = velocity;

    }

    void Run()
    {
        velocity.z = movementSettings.forwardVelocity;
    }

    void Jump()
    {
        if (jumpInput == 1 && onGround) //Pif player is on the ground hit jumping
        {
            //Jumping is on y axis
            velocity.y = movementSettings.jumpVelocity; //change the velocity of the ait to Rigedbody
            animator.SetTrigger("Jump"); //chnage animation

        }
        else if (jumpInput == 0 && onGround)
        {
            velocity.y = 0;

        }
        else
        {
            velocity.y -= physicsSettings.downAcceleration;
        }
        jumpInput = 0; //after jump is done change to zero again  
    }

    void CheckGround()
    {
        //To check Whethert the palyer is on the ground
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 0.5f);
        onGround = false;
        rigidbody.useGravity = true;
        foreach (var hit in hits)
        {
            if (!hit.collider.isTrigger) // in here we dont watn array to touch the object that has a clollider as trigger
            {
                if (velocity.y <= 0) // checking
                {
                    rigidbody.position = Vector3.MoveTowards(rigidbody.position,
                        new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Time.deltaTime * 10);
                }
                rigidbody.useGravity = false; // when player is on the ground dont use gravity
                onGround = true;
                break; // when reached on ground
            }

        }
    }


    void InputHandling()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            jumpInput = 1;
        }
    }
}
