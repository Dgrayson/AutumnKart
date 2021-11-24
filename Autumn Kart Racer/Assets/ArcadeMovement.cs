using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    private float forwardAccel, revAccel, maxSpeed, turnStrengh, forceMultiplier;

    private Vector3 move; 
    
    void Start()
    {
        body.transform.parent = null; 
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, move.x * turnStrengh * Time.deltaTime * move.y, 0f)); 

        transform.position = body.transform.position; 
    }

    private void FixedUpdate()
    {
        if(move.y > 0)
            body.AddForce(transform.forward * move.y * forwardAccel * forceMultiplier); 
        else if(move.y < 0)
            body.AddForce(transform.forward * move.y * revAccel * forceMultiplier);

    }
}
