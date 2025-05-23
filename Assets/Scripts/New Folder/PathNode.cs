using UnityEngine;

public class PathNode
{
    public Vector3Int position;
    public PathNode parent;
    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;

    public PathNode(Vector3Int pos)
    {
        position = pos;
    }
}

