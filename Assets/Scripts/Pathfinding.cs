using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoord;
    [SerializeField] Vector2Int endCoord;

    Node currentNode;
    Node startNode;
    Node endNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }

    }

    private void Start()
    {
        startNode = gridManager.Grid[startCoord];
        endNode = gridManager.Grid[endCoord];

        BreadthFirstSearch();
        BuildPath();
    }

    void ExploreNeighbors()
    {

        Shuffle();
        List<Node> neighbors = new List<Node>();

        //Check each direction from the current node
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentNode.coord + direction;

            //Chack that the neighbor is actually in the grid (i.e. not off the map)
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);

            }
        }

        //Check each neighboring Node and see if it can added to the "frontier" of the search
        foreach (Node neighbor in neighbors)
        {
            //Don't add duplicate Nodes or Nodes that cannot be traversed
            if (!reached.ContainsKey(neighbor.coord) && neighbor.isRoutable)
            {
                neighbor.connectedTo = currentNode;
                reached.Add(neighbor.coord, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool flag = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoord, startNode);

        while(frontier.Count > 0 && flag)
        {
            currentNode = frontier.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbors();
            if(currentNode.coord == endCoord)
            {
                flag = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node curNode = endNode;

        path.Add(curNode);
        curNode.isInRoute = true;

        while(curNode.connectedTo != null)
        {
            curNode = curNode.connectedTo;

            path.Add(curNode);
            curNode.isInRoute = true;
        }

        path.Reverse();

        return path;
    }


    //Shuffle the list of directions
    void Shuffle()
    {
        for(int i = 0; i < directions.Count; i++)
        {
            Vector2Int tmp = directions[i];
            int rand = Random.Range(i, directions.Count);
            directions[i] = directions[rand];
            directions[rand] = tmp;
        }
    }

}
