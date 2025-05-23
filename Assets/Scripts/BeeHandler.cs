using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BeeHandler : MonoBehaviour
{
    public Tilemap collisionTilemap;
    public Transform player;

    public float moveSpeed = 2f;
    private Queue<Vector3Int> path = new Queue<Vector3Int>();
    private Vector3Int targetCell;

    void Update()
    {
        Vector3Int beeCell = collisionTilemap.WorldToCell(transform.position);
        Vector3Int playerCell = collisionTilemap.WorldToCell(player.position);

        if (path.Count == 0 || targetCell != playerCell)
        {
            targetCell = playerCell;
            path = BFS(beeCell, playerCell);
        }

        if (path.Count > 0)
        {
            Vector3 targetWorld = collisionTilemap.GetCellCenterWorld(path.Peek());
            transform.position = Vector3.MoveTowards(transform.position, targetWorld, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWorld) < 0.01f)
            {
                path.Dequeue(); // Move to next step
            }
        }
    }

    Queue<Vector3Int> BFS(Vector3Int start, Vector3Int goal/*, int levels*/)
    {

        //Queue<Tuple<Vector3Int, int>> frontier = new Queue<Tuple<Vector3Int, int>>();
        //frontier.Enqueue(Tuple.Create(start, 0));
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(start);

        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        cameFrom[start] = start;

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
        };

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            if (current == goal)
                break;

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;

                if (!cameFrom.ContainsKey(next) && IsWalkable(next) && !IsPrefabWall(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        // Reconstruct path
        Queue<Vector3Int> path = new Queue<Vector3Int>();
        if (!cameFrom.ContainsKey(goal))
            return path; // No path

        Vector3Int step = goal;
        while (step != start)
        {
            path.Enqueue(step);
            step = cameFrom[step];
        }

        // Reverse the path
        var reversed = new Queue<Vector3Int>();
        foreach (var pos in path)
        {
            reversed.Enqueue(pos);
        }

        return new Queue<Vector3Int>(reversed.Reverse());
    }

    bool IsWalkable(Vector3Int cellPos)
    {
        return !collisionTilemap.HasTile(cellPos); // treat non-empty tiles as walls
    }
    bool IsPrefabWall(Vector3Int cell)
    {
        // Check if there is a prefab at the given cell
        Vector3 worldPos = collisionTilemap.GetCellCenterWorld(cell);

        // Cast a ray to check for a prefab at the world position (or use a collider check)
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, LayerMask.GetMask("Walls"));

        // Return true if we hit a prefab wall (ensure prefab has a collider and is on the Wall layer)
        return hit.collider != null;
    }
}
