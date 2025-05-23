using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public Transform player;
    public Tilemap wallTilemap;
    public float moveSpeed = 2f;
    public float updateRate = 1f;

    private Pathfinding pathfinder;
    private List<Vector3Int> path;
    private int currentPathIndex;

    void Start()
    {
        pathfinder = GetComponent<Pathfinding>();
        InvokeRepeating(nameof(UpdatePath), 0f, updateRate);
    }

    void UpdatePath()
    {
        path = pathfinder.FindPath(transform.position, player.position);
        currentPathIndex = 0;
    }

    void Update()
    {
        if (path == null || currentPathIndex >= path.Count) return;

        Vector3 targetPos = wallTilemap.GetCellCenterWorld(path[currentPathIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentPathIndex++;
        }
    }



    void GameOver()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");

    }


}
