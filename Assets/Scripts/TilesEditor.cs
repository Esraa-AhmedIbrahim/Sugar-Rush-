using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesEditor : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] targetTile;
    public GameObject[] prefabToPlace;
    private Dictionary<TileBase, GameObject> dict;
    void Start()
    {

        dict = new Dictionary<TileBase, GameObject>();
        for (int i = 0; i < targetTile.Length; i++)
        {
            dict.Add(targetTile[i], prefabToPlace[i]);
        }
        ReplaceTilesWithPrefabs();
    }

    void ReplaceTilesWithPrefabs()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile == null) continue;
            if (dict.ContainsKey(tile))
            {
                Vector3 worldPos = tilemap.CellToWorld(pos) + tilemap.tileAnchor;

                GameObject spawned = Instantiate(dict[tile], worldPos, dict[tile].transform.rotation);
                spawned.transform.localScale = new Vector3(tilemap.layoutGrid.cellSize.x, tilemap.layoutGrid.cellSize.y, 1f);
                tilemap.SetTile(pos, null);
            }
        }
    }


}
