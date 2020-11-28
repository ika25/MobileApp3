using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private Vector3 target = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(player.position.x, (player.position.y + 10) / 1.5f, player.position.z - 25);
        transform.position = Vector3.Lerp(transform.position, target, 1f);

    }
}
