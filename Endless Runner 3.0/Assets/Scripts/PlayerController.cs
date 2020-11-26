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

    public MovementSettings movementSettings = new MovementSettings();


    private Vector3 velocity;
    private Rigidbody ridgidbody;


    // Start is called before the first frame update
    void Start()
    {
        ridgidbody = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        Run();
        ridgidbody.velocity = velocity;

    }
    void Run()
    {
        velocity.z = movementSettings.forwardVelocity;
    }
}
