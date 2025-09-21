using UnityEngine;

public class GameGrid
{
    private int width, height;
    private GridObject[,] gridObjects;

    public int Width => width;
    public int Height => height;
    public GridObject[,] GetGridObjects() => gridObjects;

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
                Vector2Int cellPosition = new Vector2Int(x, y);
                gridObjects[x, y] = new GridObject(cellPosition);
            }
        }
    }
}
