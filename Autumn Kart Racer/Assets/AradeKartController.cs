using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AradeKartController : MonoBehaviour
{

    [SerializeField] private float currSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerate;

    [SerializeField] private Rigidbody body;

    private float verticalDir;
    private float horizontalDir;

    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        currSpeed = Mathf.SmoothStep(currSpeed, accelerate, Time.deltaTime * 2); 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Accelerate();
        Steer(); 
    }

    private void GetInput()
    {
        verticalDir = Input.GetAxis("Vertical");
        horizontalDir = Input.GetAxis("Horizontal"); 
    }

    private void Accelerate()
    {
        body.AddForce(transform.forward * currSpeed, ForceMode.Acceleration);
    }

    private void Steer()
    {

    }
}
