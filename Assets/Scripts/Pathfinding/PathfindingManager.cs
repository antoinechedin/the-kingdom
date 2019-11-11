using System;
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
                }
            }
        }
    }

    public List<Cell> FindPath(Vector2 start, Vector2 target)
    {

        foreach (Cell cell in grid)
        {
            if (cell == null) continue;
            cell.gCost = Mathf.Infinity;
            cell.fCost = Mathf.Infinity;
            cell.previousCell = null;
            if (DebugMode.Instance.debug)
                SetCellColor(cell, Color.cyan);
        }

        Cell startCell = getCellfromWorldPos(start);
        Cell targetCell = getCellfromWorldPos(target);

        if (startCell == null || targetCell == null)
        {
            List<Cell> emtyPath = new List<Cell>();
            emtyPath.Add(startCell);
            return emtyPath;
        }

        List<Cell> openSet = new List<Cell>();
        List<Cell> closedSet = new List<Cell>();

        startCell.gCost = 0f;
        startCell.fCost = GetCellDistance(startCell, targetCell);
        openSet.Add(startCell);

        while (openSet.Count > 0)
        {
            Cell currentCell = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
                if (currentCell.fCost > openSet[i].fCost)
                    currentCell = openSet[i];

            if (currentCell == targetCell)
                return ReconstructPath(currentCell);

            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            foreach (Cell neighbor in GetNeighbors(currentCell))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newNeighborGCost = currentCell.gCost
                    + GetCellDistance(currentCell, neighbor);
                if (newNeighborGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.previousCell = currentCell;
                    neighbor.gCost = newNeighborGCost;
                    neighbor.fCost = newNeighborGCost
                        + GetCellDistance(neighbor, targetCell);
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // No path found, return start Cell
        List<Cell> path = new List<Cell>();
        path.Add(startCell);
        return path;
    }

    private List<Cell> ReconstructPath(Cell cell)
    {
        List<Cell> path = new List<Cell>();
        while (cell.previousCell != null)
        {
            if (DebugMode.Instance.debug)
                SetCellColor(cell, Color.red);
            path.Add(cell);
            cell = cell.previousCell;
        }
        path.Reverse();
        return path;
    }

    public Cell getCellfromWorldPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x) - bounds.xMin;
        int y = Mathf.FloorToInt(worldPos.y) - bounds.yMin;

        if (x >= 0 && x < bounds.size.x && y >= 0 && y < bounds.size.y)
            return grid[x, y];
        return null;
    }

    private float GetCellDistance(Cell start, Cell target)
    {
        Vector2 distance = new Vector2(target.x - start.x, target.y - start.y);
        return distance.magnitude;
    }

    public List<Cell> GetNeighbors(Cell cell)
    {
        bool right = false, left = false, top = false, down = false;
        List<Cell> neighbors = new List<Cell>();
        if (cell.x - 1 >= 0 && grid[cell.x - 1, cell.y] != null)
        {
            left = true;
            neighbors.Add(grid[cell.x - 1, cell.y]);
        }
        if (cell.x + 1 < bounds.size.x && grid[cell.x + 1, cell.y] != null)
        {
            right = true;
            neighbors.Add(grid[cell.x + 1, cell.y]);
        }
        if (cell.y - 1 >= 0 && grid[cell.x, cell.y - 1] != null)
        {
            down = true;
            neighbors.Add(grid[cell.x, cell.y - 1]);
        }
        if (cell.y + 1 < bounds.size.y && grid[cell.x, cell.y + 1] != null)
        {
            top = true;
            neighbors.Add(grid[cell.x, cell.y + 1]);
        }

        if (left && top && grid[cell.x - 1, cell.y + 1] != null)
            neighbors.Add(grid[cell.x - 1, cell.y + 1]);

        if (right && top && grid[cell.x + 1, cell.y + 1] != null)
            neighbors.Add(grid[cell.x + 1, cell.y + 1]);

        if (right && down && grid[cell.x + 1, cell.y - 1] != null)
            neighbors.Add(grid[cell.x + 1, cell.y - 1]);

        if (left && down && grid[cell.x - 1, cell.y - 1] != null)
            neighbors.Add(grid[cell.x - 1, cell.y - 1]);

        return neighbors;
    }

    private void SetCellColor(Cell cell, Color color)
    {
        Vector3Int gridPos = new Vector3Int(bounds.xMin + cell.x, bounds.yMin + cell.y, 0);
        tilemap.SetTileFlags(gridPos, TileFlags.None);
        tilemap.SetColor(gridPos, color);
    }
}


public class Cell
{
    public int x, y;
    public Vector3 worldPos;
    public float gCost, fCost;
    public Cell previousCell;

    public Cell(int x, int y, Vector2 worldPos)
    {
        this.x = x;
        this.y = y;
        this.worldPos = worldPos;
    }
}
