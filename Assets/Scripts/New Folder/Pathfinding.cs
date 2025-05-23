using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    public Tilemap wallTilemap;

    public List<Vector3Int> FindPath(Vector3 startWorld, Vector3 targetWorld)
    {
        Vector3Int start = wallTilemap.WorldToCell(startWorld);
        Vector3Int target = wallTilemap.WorldToCell(targetWorld);

        List<PathNode> openList = new List<PathNode>();   // node to visit

        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>(); //node that visited

        PathNode startNode = new PathNode(start);

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.FCost.CompareTo(b.FCost));

            PathNode current = openList[0];

            if (current.position == target)
            {
                return ReconstructPath(current);
            }

            openList.Remove(current);
            closedSet.Add(current.position);

            foreach (Vector3Int dir in GetDirections())
            {
                Vector3Int neighborPos = current.position + dir;

                if (closedSet.Contains(neighborPos)) continue;
                if (wallTilemap.HasTile(neighborPos)) continue; // Wall = not walkable

                PathNode neighbor = new PathNode(neighborPos);
                neighbor.gCost = current.gCost + 1;
                neighbor.hCost = Vector3Int.Distance(neighborPos, target);
                neighbor.parent = current;

                bool inOpen = openList.Exists(n => n.position == neighborPos);
                if (!inOpen) openList.Add(neighbor);
            }
        }

        return null; // No path found
    }

    private List<Vector3Int> ReconstructPath(PathNode endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        PathNode current = endNode;
        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    private List<Vector3Int> GetDirections()
    {
        return new List<Vector3Int> {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };
    }
}
