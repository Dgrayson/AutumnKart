using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class KartController : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float currSteeringAngle;

    [SerializeField] private WheelCollider frontDriverCollider, frontPassengerCollider;
    [SerializeField] private WheelCollider rearDriverCollider, rearPassengerCollider;

    [SerializeField] private Transform frontDriverT, frontPassengerT;
    [SerializeField] private Transform rearDriverT, rearPassengerT;

    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float motorForce;

    [SerializeField] private TextMeshProUGUI speedText;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        UpdateText(); 
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); 
    }

    private void Accelerate()
    {
        frontDriverCollider.motorTorque = verticalInput * motorForce;
        frontPassengerCollider.motorTorque = verticalInput * motorForce;
        /*rearPassengerCollider.motorTorque = verticalInput * motorForce;

        rearDriverCollider.motorTorque = verticalInput * motorForce;*/

    }

    private void Steer()
    {
        currSteeringAngle = maxSteeringAngle * horizontalInput;
        frontDriverCollider.steerAngle = currSteeringAngle;
        frontPassengerCollider.steerAngle = currSteeringAngle; 

    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverCollider, frontDriverT);
        UpdateWheelPose(frontPassengerCollider, frontPassengerT);
        /*UpdateWheelPose(rearDriverCollider, rearDriverT);
        UpdateWheelPose(rearPassengerCollider, rearPassengerT);*/
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform) 
    {
        Vector3 pos = _transform.position;
        Quaternion quat = _transform.rotation;

        _collider.GetWorldPose(out pos, out quat);

        _transform.position = pos;
        _transform.rotation = quat; 
    }

    private void UpdateText()
    {
        speedText.text = "Speed: " + Mathf.Round(body.velocity.magnitude).ToString(); 
    }
}
