using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKartController : MonoBehaviour
{
    [SerializeField] private float steerDirection;
    [SerializeField] private float currSteeringAngle;

    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float motorForce;
    [SerializeField] private float acceleration = 1.0f;

    [SerializeField] private int nextNode = 2;

    [Header("Wheels")]
    [SerializeField] private WheelCollider frontDriverCollider, frontPassengerCollider;
    [SerializeField] private WheelCollider rearDriverCollider, rearPassengerCollider;

    [SerializeField] private Transform frontDriverT, frontPassengerT;
    [SerializeField] private Transform rearDriverT, rearPassengerT;

    [Header("Sight")]
    [SerializeField] private float sightDistance = 15;
    [SerializeField] private bool approachingWall = false;
    [SerializeField] private LayerMask wallMask; 

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckWalls(); 
        CalculateSteeringDirection(); 
        Steer();
        Accelerate();
        UpdateWheelPoses();
        CheckNodeDist(); 
    }

    private void CheckWalls()
    {
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-45, transform.up) * transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-30, transform.up) * transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-15, transform.up) * transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(15, transform.up) * transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(30, transform.up) * transform.forward * sightDistance, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(45, transform.up) * transform.forward * sightDistance, Color.blue);

        RaycastHit hit; 

        /*if(Physics.Raycast(transform.position, transform.forward * sightDistance, out hit, wallMask))
        {
            Debug.Log("COlliding wth : " + hit.transform.name);
            approachingWall = true; 
        }
        else
        {
            approachingWall = false;
        }*/
    }
    private void CalculateSteeringDirection()
    {
        Vector3 dirToNode = transform.InverseTransformPoint(TrackNodes.Instance.nodes[nextNode].transform.position);

        dirToNode = dirToNode / dirToNode.magnitude;

        steerDirection = dirToNode.x / dirToNode.magnitude; 
    }

    private void Accelerate()
    {
        if (approachingWall)
        {
            acceleration = .5f;
        }
        else
            acceleration = 1.0f; 

        frontDriverCollider.motorTorque = acceleration * motorForce;
        frontPassengerCollider.motorTorque = acceleration * motorForce;
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

        //Debug.Log("Distance to node: " + currNodeDist);

        if (currNodeDist <= 0)
        {
            
            if (nextNode >= TrackNodes.Instance.nodes.Count || nextNode == TrackNodes.Instance.nodes.Count - 1)
                nextNode = 0; 
            else
                nextNode++;
        }
    }
}
