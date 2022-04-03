using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridDimensions;
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return UnityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coord)
    {
        if (grid.ContainsKey(coord))
        {
            return grid[coord];
        }

        return null;
    }

    public void BlockNode(Vector2Int coord)
    {
        if(grid.ContainsKey(coord))
        {
            grid[coord].isRoutable = false;
        }
    }

    public Vector2Int CoordFromPos(Vector3 pos)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(pos.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(pos.z / unityGridSize);

        return coordinates;
    }

    public Vector3 PosFromCoord(Vector2Int coord)
    {
        Vector3 pos = new Vector3();

        pos.x = coord.x * unityGridSize;
        pos.z = coord.y * unityGridSize;

        return pos;
    }

    void CreateGrid()
    {
        for(int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                grid.Add(coord, new Node(coord, true));
            }
        }
    }
}
