using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool walkable = true;

	// Use this for initialization
	void Start () {
        this.resetNode();
    }

    /// <summary>
    /// Reset all the values in the nodes.
    /// </summary>
    public void resetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
        this.reloadNeighbourNode();
    }

    // -------------------------------- Setters --------------------------------

    /// <summary>
    /// Set the parent node.
    /// </summary>
    /// <param name="node">Set the node for parent node.</param>
    public void setParentNode(Transform node)
    {
        this.parentNode = node;
    }

    /// <summary>.
    /// Set the weight value
    /// </summary>
    /// <param name="value">weight value</param>
    public void setWeight(float value)
    {
        this.weight = value;
    }

    /// <summary>
    /// Set is node is walkable.
    /// </summary>
    /// <param name="value">boolean</param>
    public void setWalkable(bool value)
    {
        this.walkable = value;
    }

    // -------------------------------- Getters --------------------------------

    /// <summary>
    /// Get neighbour node.
    /// </summary>
    /// <returns>All the nodes.</returns>
    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }

    /// <summary>
    /// Get weight
    /// </summary>
    /// <returns>get weight in float.</returns>
    public float getWeight()
    {
        float result = this.weight;
        return result;

    }

    /// <summary>
    /// Get the parent Node.
    /// </summary>
    /// <returns>Return the parent node.</returns>
    public Transform getParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }


    // -------------------------------- Checkers --------------------------------

    /// <summary>
    /// Get if the node is walkable.
    /// </summary>
    /// <returns>boolean</returns>
    public bool isWalkable()
    {
        bool result = walkable;
        return result;
    }

    // -------------------------------- Detections --------------------------------

    /// <summary>
    /// Reload the neighbour node.
    /// </summary>
    public void reloadNeighbourNode()
    {   
        neighbourNode = this.detectNeighbourNode();
    }

    /// <summary>
	/// Detect north, south, east, west node.
	/// </summary>
	/// <returns>The detect object.</returns>
	private List<Transform> detectNeighbourNode()
    {

        List<Transform> result = new List<Transform>();

        // Where to detect.
        Vector3 back = transform.TransformDirection(Vector3.back);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 left = transform.TransformDirection(Vector3.left);

        Vector3[] direction = { back, forward, right, left };

        RaycastHit hit;

        foreach (Vector3 dir in direction)
        {
            if (Physics.Raycast(transform.position, dir, out hit))
            {   
                // Make sure to only take node with tagged.
                if (hit.transform.tag == "Node")
                {   

                    // Only need neighbour nodes that are walkable.
                    Node node = hit.transform.GetComponent<Node>();
                    if (node.isWalkable())
                    {
                        Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
                        result.Add(hit.transform);
                    }
                }
            }
        }

        return result;
    }
}
