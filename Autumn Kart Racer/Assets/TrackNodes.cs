using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackNodes : MonoBehaviour
{
    public Color lineColor;

    [SerializeField] public List<Transform> nodes = new List<Transform>();

    private static TrackNodes _instance; 

    public static TrackNodes Instance { get { return _instance; } }

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void CreateNodes()
    {
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
                nodes.Add(pathTransforms[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>(); 

        for(int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
                nodes.Add(pathTransforms[i]); 
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currNode = nodes[i].position;
            Vector3 prevNode = Vector3.zero; 

            if(i > 0)
            {
                prevNode = nodes[i - 1].position; 
            }
            else if(i == 0 && nodes.Count > 1)
            {
                prevNode = nodes[nodes.Count - 1].position; 
            }

            Gizmos.DrawLine(prevNode, currNode); 
        }

        Debug.Log("Drawing");
    }
}
