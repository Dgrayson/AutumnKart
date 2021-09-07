using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKartController : MonoBehaviour
{
    [SerializeField] private float steerDirection;
    [SerializeField] private float currSteeringAngle;

    [SerializeField] private WheelCollider frontDriverCollider, frontPassengerCollider;
    [SerializeField] private WheelCollider rearDriverCollider, rearPassengerCollider;

    [SerializeField] private Transform frontDriverT, frontPassengerT;
    [SerializeField] private Transform rearDriverT, rearPassengerT;

    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float motorForce;

    [SerializeField] private int nextNode = 2; 

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateSteeringDirection(); 
        Steer();
        Accelerate();
        UpdateWheelPoses();
        CheckNodeDist(); 
    }

    private void CalculateSteeringDirection()
    {
        Vector3 dirToNode = transform.InverseTransformPoint(TrackNodes.Instance.nodes[nextNode].transform.position);

        dirToNode = dirToNode / dirToNode.magnitude;

        steerDirection = dirToNode.x / dirToNode.magnitude; 
    }

    private void Accelerate()
    {
        frontDriverCollider.motorTorque = 1 * motorForce;
        frontPassengerCollider.motorTorque = 1 * motorForce;
    }

    private void Steer()
    {
        currSteeringAngle = maxSteeringAngle * steerDirection;
        frontDriverCollider.steerAngle = currSteeringAngle;
        frontPassengerCollider.steerAngle = currSteeringAngle;

    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverCollider, frontDriverT);
        UpdateWheelPose(frontPassengerCollider, frontPassengerT);
        UpdateWheelPose(rearDriverCollider, rearDriverT);
        UpdateWheelPose(rearPassengerCollider, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 pos = _transform.position;
        Quaternion quat = _transform.rotation;

        _collider.GetWorldPose(out pos, out quat);

        _transform.position = pos;
        _transform.rotation = quat;
    }

    private void CheckNodeDist()
    {
        Vector3 relativePos = TrackNodes.Instance.nodes[nextNode].position - transform.position; 

        float currNodeDist = Vector3.Dot(transform.forward, relativePos); 

        Debug.Log("Distance to node: " + currNodeDist);

        if (currNodeDist <= 0)
        {
            if (nextNode >= TrackNodes.Instance.nodes.Count || nextNode == TrackNodes.Instance.nodes.Count - 1)
                nextNode = 0; 
            else
                nextNode++;
        }
    }
}
