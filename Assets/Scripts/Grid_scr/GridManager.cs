using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    public static GridManager Instance = instance;

    [SerializeField] private Tilemap tilemap;

    private GameGrid grid;
    private GridRenderer gridRenderer;
    private bool showGrid;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        InitializeGrid();
    } 

    private void InitializeGrid()
    {
        if (tilemap == null) { return; }
        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
        gridRenderer = new GridRenderer(tilemap.origin);
    }

    private void OnDrawGizmos()
    {
        if (tilemap == null || grid == null || showGrid == false) { return; }

        Gizmos.color = Color.cyan;

        GridObject[,] gridObjects = grid.GetGridObjects();

        foreach (GridObject gridObject in gridObjects)
        {
            int cellPosX = gridObject.GetCellPosition.x;
            int cellPosy = gridObject.GetCellPosition.y;

            int xPos = (int)gridRenderer.GetWorldPosition(cellPosX, cellPosy).x;
            int yPos = (int)gridRenderer.GetWorldPosition(cellPosX, cellPosy).y;

            Vector3 startVertical = new Vector3(xPos, yPos);
            Vector3 endVertical = new Vector3(xPos, yPos + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = new Vector3(xPos, yPos);
            Vector3 endHorizontal = new Vector3(xPos + 1, yPos);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            textStyle.alignment = TextAnchor.MiddleCenter;

            Vector3 cellCenter = new Vector3(xPos + 0.5f, yPos + 0.5f);
            Handles.Label(cellCenter, text: $"{cellPosX}, {cellPosy}", textStyle);
        }

        int tilemapWidth = grid.Width;
        int tilemapHeight = grid.Height;

        Vector3 tilemapEndWorldSpace = gridRenderer.GetWorldPosition(tilemapWidth, tilemapHeight);

        float originWorldSpaceX = gridRenderer.GetWorldPosition(0, 0).x;
        float originWorldSpaceY = gridRenderer.GetWorldPosition(0, 0).y;

        float widthWorldSpace = tilemapEndWorldSpace.x;
        float heightWorldSpace = tilemapEndWorldSpace.y;

        Vector3 finalStartVertical = new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 finalEndVertical = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartVertical, finalEndVertical);

        Vector3 finalStartHorizontal = new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 finalEndHorizontal = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartHorizontal, finalEndHorizontal);
    }

    public void RegenerateGrid() => InitializeGrid();

    public void ToggleVisibility() => showGrid = !showGrid;
}
