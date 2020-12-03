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
    public float Xspeed = 10;
    public bool IsDead = false;


    private Vector3 velocity;
    private Rigidbody rigidbody;
    private Animator animator;
    private int jumpInput = 0, slideInput = 0;
    public bool onGround = false;
    public float distanceToGround;
    private float xmovement = 0;
    // Collider veriables
    private CapsuleCollider collider;
    private float colliderSize;
    private float colliderHight;
    private Vector3 colliderCenter;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        collider = GetComponent<CapsuleCollider>();
        colliderHight = collider.height;
        colliderCenter = collider.center;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputHandling();
        Run();
       Debug.Log (CheckGround());
        ColliderHandling();
        Jump();
        Slide();
        MoveX();
        rigidbody.velocity = velocity;

    }

    void ColliderHandling()
    {
        if (onGround) //colider slide function wheen on ground
        {
            colliderSize = animator.GetFloat("ColliderSize");
            if (colliderSize > 0.2f && colliderSize < 1) // Shrink
            {
                
                collider.height = 2;
                collider.center = new Vector3(collider.center.x, 0.85f, collider.center.z);
            }
            else // Reset
            {
                collider.height = colliderHight;
                collider.center = colliderCenter;
                
            }
        }
        else // colider jump function when jumping
        {
            colliderSize = animator.GetFloat("ColliderSize");
            if (colliderSize > 0.1f && colliderSize < 1) // Shrink
            {
                
                collider.height = 3.2f;
                collider.center = new Vector3(collider.center.x, 4.2f, collider.center.z);
            }
            else // Reset
            {
                
                collider.height = colliderHight;
                collider.center = colliderCenter;
            }
        }
    }

    void Run()
    {
        velocity.z = movementSettings.forwardVelocity;
    }

    void Slide()
    {
        if (slideInput == 1 && CheckGround()) // slide should be done when player is on the ground
        {
            animator.SetTrigger("Slide");
            slideInput = 0;
        }
    }

    void Jump()
    {
        if (jumpInput == 1 && CheckGround()) //Pif player is on the ground hit jumping
        {
            Debug.Log("JUMP");
            //Jumping is on y axis
            velocity.y = movementSettings.jumpVelocity; //change the velocity of the ait to Rigedbody
            animator.SetTrigger("Jump"); //chnage animation

        }
        else if (jumpInput == 0 && CheckGround())
        {
            velocity.y = 0;

        }
        else
        {
            velocity.y -= physicsSettings.downAcceleration;
        }
        jumpInput = 0; //after jump is done change to zero again  
    }

    bool CheckGround()
    {

        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
       
    }

    void MoveX()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xmovement, transform.position.y, transform.position.z), Time.deltaTime * Xspeed);
    }


    void InputHandling()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Jump
        {

            jumpInput = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) // Slide
        {
            slideInput = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Right
        {
            if (xmovement == 0) // player is in the middle
            {
                xmovement = 5;
                animator.SetTrigger("RightMove");
            }
            else if (xmovement == -5) // player is in the left
            {
                xmovement = 0;
                animator.SetTrigger("RightMove");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) // left
        {
            if (xmovement == 0) // player is in the middle
            {
                xmovement = -5;
                animator.SetTrigger("LeftMove");
            }
            else if (xmovement == 5) // player is in the right
            {
                xmovement = 0;
                animator.SetTrigger("LeftMove");
            }
        }
    }
        public void KillPlayer()
        {
            IsDead = true;
            movementSettings.forwardVelocity = 0;
            animator.SetTrigger("Die");
        }
}


