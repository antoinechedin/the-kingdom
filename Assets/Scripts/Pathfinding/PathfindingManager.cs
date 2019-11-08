using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingManager : MonoBehaviour
{
    public Tilemap tilemap;

    private Cell[,] grid;
    private BoundsInt bounds;

    private void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Cell[bounds.size.x, bounds.size.y];
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                if (tilemap.HasTile(pos))
                {
                    grid[x, y] = new Cell(x, y, new Vector2(pos.x + 0.5f, pos.y + 0.5f));
                    Debug.Log(grid[x, y].worldPos);
                }
            }
        }
    }

    public Cell getCellfromWorldPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x) - bounds.xMin;
        int y = Mathf.FloorToInt(worldPos.y) - bounds.yMin;

        if (x >= 0 && x < bounds.size.x && y >= 0 && y < bounds.size.y)
            return grid[x, y];
        return null;

    }
}

public class Cell
{
    public int x, y;
    public Vector2 worldPos;

    public Cell(int x, int y, Vector2 worldPos)
    {
        this.x = x;
        this.y = y;
        this.worldPos = worldPos;
    }
}
