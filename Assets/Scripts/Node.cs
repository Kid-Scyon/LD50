using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coord;
    public bool isRoutable;
    public bool isExplored;
    public bool isInRoute;
    public bool isResource;
    public Node connectedTo;

    public Node(Vector2Int coord, bool isRoutable)
    {
        this.coord = coord;
        this.isRoutable = isRoutable;
    }
}
