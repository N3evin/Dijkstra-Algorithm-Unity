using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    public int row = 5;
    public int column = 5;
    public float padding = 3f;
    public Transform nodePrefab;

    public List<Transform> grid = new List<Transform>();

	// Use this for initialization
	void Start () {
        this.generateGrid();
        this.generateNeighbours();
	}
	
    /// <summary>
    /// Generate the grid with the node.
    /// </summary>
	private void generateGrid()
    {
        for(int i = 0; i < column; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Transform node = Instantiate(nodePrefab, new Vector3((j * padding) + gameObject.transform.position.x, 0, (i * padding) + gameObject.transform.position.z), Quaternion.identity);
                grid.Add(node);
            }
        }

        
    }

    /// <summary>
    /// Generate the neighbours for each node.
    /// </summary>
    private void generateNeighbours()
    {
        for(int i = 0; i < grid.Count; i++)
        {
            Node currentNode = grid[i].GetComponent<Node>();
            int index = i + 1;
            
            // For those on the left, with no left neighbours
            if(index%column == 1)
            {
                string name = "";
                // We want the node at the top as long as there is a node.
                if (i + column < column*row)
                {
                    name += "N, ";
                    currentNode.addNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    name += "S, ";
                    currentNode.addNeighbourNode(grid[i - column]);   // South node
                }
                name += "E";
                currentNode.addNeighbourNode(grid[i + 1]);     // East node
                grid[i].name = name;
            }
            
            // For those on the right, with no right neighbours
            else if (index%column == 0)
            {
                string name = "";
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    name += "N, ";
                    currentNode.addNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    name += "S, ";
                    currentNode.addNeighbourNode(grid[i - column]);   // South node
                }
                name += "W";
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
                grid[i].name = name;
            }

            else
            {
                string name = "";
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    name += "N, ";
                    currentNode.addNeighbourNode(grid[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    name += "S, ";
                    currentNode.addNeighbourNode(grid[i - column]);   // South node
                }
                name += "E, W";
                currentNode.addNeighbourNode(grid[i + 1]);     // East node
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
                grid[i].name = name;
            }

        }
    }
}
