using System;
using UnityEngine;

public class GameGrid
{
    private int width, height;
    private GridObject[,] gridObjects;

    public int Width => width;
    public int Height => height;
    public GridObject[,] GetGridObjects() => gridObjects;

    public GridObject GetGridObject(GridCell cell) => gridObjects[cell.X, cell.Y]; 

    public GameGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridObjects = new GridObject[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GridCell cellPosition = new GridCell(x, y);
                gridObjects[x, y] = new GridObject(cellPosition);
            }
        }
    }

    public bool IsValidCell(GridCell cellToCheck)
    {
        if (cellToCheck.X > width || cellToCheck.Y > height) return false;
        if (cellToCheck.X < 0 || cellToCheck.Y < 0) return false;

        return true;
    }

    public GridCell GetNeighbourCell(GridCell currentCell, Direction direction)
    {
        GridCell neighbourCell;

        switch (direction)
        {
            case Direction.Up:
                neighbourCell = new GridCell(currentCell.X, currentCell.Y + 1);
                break;
            case Direction.Right:
                neighbourCell = new GridCell(currentCell.X + 1, currentCell.Y);
                break;
            case Direction.Down:
                neighbourCell = new GridCell(currentCell.X, currentCell.Y - 1);
                break;
            case Direction.Left:
                neighbourCell = new GridCell(currentCell.X - 1, currentCell.Y);
                break;
            default:
                throw new Exception("Invalid direction in GetNeighboutCell");
        }

        if (!IsValidCell(neighbourCell)) { throw new Exception("Neighbour Cell is outside the grid"); }

        return neighbourCell;
    }
}

public readonly struct GridCell
{
    public int X { get; }
    public int Y { get; }

    public GridCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum Direction { Up, Down, Left, Right, Invalid }