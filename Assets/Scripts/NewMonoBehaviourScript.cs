using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinPlacer : MonoBehaviour
{
    public Tilemap coinTilemap;           
    public TileBase coinTile;            
    public GameObject coinPrefab;        

    void Start()
    {
        PlaceCoins();
    }

    void PlaceCoins()
    {
        BoundsInt bounds = coinTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = coinTilemap.GetTile(cellPosition);

                if (tile == coinTile)
                {
                    Vector3 worldPos = coinTilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0); 
                    Instantiate(coinPrefab, worldPos, Quaternion.identity);
                }
            }
        }

        
    }
}
