using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public partial class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private Tilemap tilemap;

    private GameGrid grid;
    private GridRenderer gridRenderer;
    private bool showGrid;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeGrid();
    } 

    private void InitializeGrid()
    {
        if (tilemap == null) { return; }
        tilemap.CompressBounds();

        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
        gridRenderer = new GridRenderer(tilemap.origin, grid);

        foreach (GridObject gridObject in grid.GetGridObjects())
        {
            int xPos = gridObject.GetCellPosition.X;
            int yPos = gridObject.GetCellPosition.Y;
            Vector3 objectWorldPosition = gridRenderer.GetWorldPosition(new GridCell(xPos, yPos));

            int xVal = Mathf.FloorToInt(objectWorldPosition.x);
            int yVal = Mathf.FloorToInt(objectWorldPosition.y);
            TileBase tile = tilemap.GetTile(new Vector3Int(xVal, yVal, 0));

            if (tile == null) { continue; }

            Type tileType = tile.GetType();

            if (tileType == typeof(Wall))
            {
                gridObject.Type = GridObjectType.Wall;
            }

            if (tileType == typeof(Path))
            {
                gridObject.Type = GridObjectType.Path;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (tilemap == null || grid == null || showGrid == false) { return; }

        Gizmos.color = Color.cyan;

        GridObject[,] gridObjects = grid.GetGridObjects();

        foreach (GridObject gridObject in gridObjects)
        {
            int cellPosX = gridObject.GetCellPosition.X;
            int cellPosy = gridObject.GetCellPosition.Y;

            int xPos = (int)gridRenderer.GetWorldPosition(gridObject.GetCellPosition).x;
            int yPos = (int)gridRenderer.GetWorldPosition(gridObject.GetCellPosition).y;

            Vector3 startVertical = new Vector3(xPos, yPos);
            Vector3 endVertical = new Vector3(xPos, yPos + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = new Vector3(xPos, yPos);
            Vector3 endHorizontal = new Vector3(xPos + 1, yPos);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            Vector3 cellCenter = new Vector3(xPos + 0.5f, yPos + 0.5f);

            if (gridObject.Type == GridObjectType.Wall)
            {
                GUIStyle textStyle = new GUIStyle();
                textStyle.fontSize = 12;
                textStyle.normal.textColor = Color.green;
                textStyle.fontStyle = FontStyle.Bold;
                textStyle.alignment = TextAnchor.MiddleCenter;
                Handles.Label(cellCenter, text: $"Wall", textStyle);
            }
            else
            {
                GUIStyle textStyle = new GUIStyle();
                textStyle.normal.textColor = Color.white;
                textStyle.alignment = TextAnchor.MiddleCenter;
                Handles.Label(cellCenter, text: $"{cellPosX},{cellPosy}", textStyle);
            }
        }

        int tilemapWidth = grid.Width;
        int tilemapHeight = grid.Height;

        Vector3 tilemapEndWorldSpace = gridRenderer.GetWorldPosition(new GridCell(tilemapWidth, tilemapWidth));

        float originWorldSpaceX = gridRenderer.GetWorldPosition(new GridCell(0, 0)).x;
        float originWorldSpaceY = gridRenderer.GetWorldPosition(new GridCell(0, 0)).y;

        float widthWorldSpace = tilemapEndWorldSpace.x;
        float heightWorldSpace = tilemapEndWorldSpace.y;

        Vector3 finalStartVertical = new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 finalEndVertical = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartVertical, finalEndVertical);

        Vector3 finalStartHorizontal = new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 finalEndHorizontal = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartHorizontal, finalEndHorizontal);
    }

#endif
    public void RegenerateGrid() => InitializeGrid();

    public void ToggleVisibility() => showGrid = !showGrid;
}
