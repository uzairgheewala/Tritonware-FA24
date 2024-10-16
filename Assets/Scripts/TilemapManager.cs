using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public Tilemap backgroundTilemap;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public Tilemap objectTilemap;

    public TileBase backgroundTile;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase objectTile;

    public void InitializeRoom(Vector3Int startPosition, int width, int height)
    {
        for (int x = startPosition.x; x < startPosition.x + width; x++)
        {
            for (int y = startPosition.y; y < startPosition.y + height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                // Background tile
                backgroundTilemap.SetTile(tilePosition, backgroundTile);

                // Floor and Walls
                if (x == startPosition.x || x == startPosition.x + width - 1 ||
                    y == startPosition.y || y == startPosition.y + height - 1)
                {
                    wallTilemap.SetTile(tilePosition, wallTile);
                }
                else
                {
                    floorTilemap.SetTile(tilePosition, floorTile);
                }
            }
        }

        Logger.Log($"Initialized room at {startPosition} with size {width}x{height}");
    }

    public void ClearRoom(Vector3Int startPosition, int width, int height)
    {
        for (int x = startPosition.x; x < startPosition.x + width; x++)
        {
            for (int y = startPosition.y; y < startPosition.y + height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                backgroundTilemap.SetTile(tilePosition, null);
                floorTilemap.SetTile(tilePosition, null);
                wallTilemap.SetTile(tilePosition, null);
                objectTilemap.SetTile(tilePosition, null);
            }
        }

        Logger.Log($"Cleared room at {startPosition} with size {width}x{height}");
    }

    public void PlaceObject(Vector3Int position)
    {
        objectTilemap.SetTile(position, objectTile);
        Logger.Log($"Placed object at {position}");
    }

    public void RemoveObject(Vector3Int position)
    {
        objectTilemap.SetTile(position, null);
        Logger.Log($"Removed object from {position}");
    }
}