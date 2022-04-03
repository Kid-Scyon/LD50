using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoord;
    public Vector2Int StartCoord { get { return startCoord; } }

    [SerializeField] Vector2Int endCoord;
    public Vector2Int EndCoord { get { return endCoord; } }

    Node currentNode;
    Node startNode;
    Node endNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    GridManager gridManager;

    ObjectPool pool;
    List<Vector2Int> spawnLocations = new List<Vector2Int>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pool = FindObjectOfType<ObjectPool>();
        
        foreach(GameObject g in pool.enemySpawners)
        {
            spawnLocations.Add(gridManager.CoordFromPos(g.transform.position));
        }

        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoord];
            endNode = grid[endCoord];
        }

    }

    private void Start()
    {
        GetNewPath();
    }

    public void UpdateStartCoord(Vector2Int newCoord)
    {
        startCoord = newCoord;
    }

    public void UpdateEndCoord(Vector2Int newCoord)
    {
        endCoord = newCoord;
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoord);
    }

    public List<Node> GetNewPath(Vector2Int coord)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coord);
        return BuildPath();
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

    public Node ExploreNeighborsResource(Vector2Int placeLocation)
    {
        
        Shuffle();

        List<Node> neighbors = new List<Node>();

        //Check each direction from the current node
        foreach (Vector2Int direction in directions)
        {

            Vector2Int neighborCoords = placeLocation + direction;

            //Chack that the neighbor is actually in the grid (i.e. not off the map)
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        //Check each neighboring Node and see if it's a resource
        foreach (Node neighbor in neighbors)
        {
            //If we find one, it's legal and we can place
            if (neighbor.isResource)
            {
                return neighbor;
            }
        }
        return null;
        
    }

    void BreadthFirstSearch(Vector2Int coord)
    {
        startNode.isRoutable = true;
        endNode.isRoutable = true;

        frontier.Clear();
        reached.Clear();

        bool flag = true;

        frontier.Enqueue(grid[coord]);
        reached.Add(coord, grid[coord]);

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

    public bool WillBlockPath(Vector2Int coord)
    {

        if(grid.ContainsKey(coord))
        {
            bool previousState = grid[coord].isRoutable;
            grid[coord].isRoutable = false;
            foreach(Vector2Int v in spawnLocations)
            {
                List<Node> newPath = GetNewPath(v);

                if (newPath.Count <= 1)
                {
                    grid[coord].isRoutable = true;
                    GetNewPath();
                    return true;
                }
            }

        }
        grid[coord].isRoutable = true;
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
