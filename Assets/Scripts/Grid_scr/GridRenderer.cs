using UnityEngine;

public class GridRenderer
{
    private Vector3 origin;
    private float gridSize = 1.0f;

    private GameGrid grid;

    public float GridSize => gridSize;

    public GridRenderer(Vector3 origin, GameGrid grid)
    {
        this.origin = origin;
        this.grid = grid;
    }

    public Vector3 GetCellCenter(GridCell cell)
    {
        return GetWorldPosition(cell) + new Vector3(gridSize/2, gridSize/2, 0);
    }

    public Direction GetDirectionFromVector(Vector2 directionVector)
    {
        if (directionVector == Vector2.up) { return Direction.Up; }
        else if (directionVector == Vector2.down) { return Direction.Down; }
        else if (directionVector == Vector2.left) { return Direction.Left; }
        else if (directionVector == Vector2.right) { return Direction.Right; }

        return Direction.Invalid;
    }

    public Vector3 GetWorldPosition(GridCell cell)
    {
        return new Vector3(cell.X, cell.Y, 0) + origin;
    }

    public GridCell GetCellFromWorldPosition(Vector3 worldPosition)
    {
        Vector3 originReverted = worldPosition - origin;
        GridCell cellCandidate = new GridCell(Mathf.FloorToInt(originReverted.x), Mathf.FloorToInt(originReverted.y));

        if (!grid.IsValidCell(cellCandidate)) { throw new System.Exception("Invalid Cell"); }

        return cellCandidate;
    }

    public Vector3 GetPositionInWorldCoordinates(Vector3 worldPosition) => worldPosition - origin;

    public bool HasReachedCellCenterInMoveDirection(Vector2 dir, Vector3 currentPos)
    {
        Direction direction = GetDirectionFromVector(dir);
        Vector3 posInGridCoordinates = GetPositionInWorldCoordinates(currentPos);
        float xDistanceFromCellStart = posInGridCoordinates.x - Mathf.Floor(posInGridCoordinates.x);
        float yDistanceFromCellStart = posInGridCoordinates.y - Mathf.Floor(posInGridCoordinates.y);

        float distanceToCenter = gridSize / 2;

        switch (direction)
        {
            case Direction.Up: return yDistanceFromCellStart >= distanceToCenter;
            case Direction.Down: return yDistanceFromCellStart <= distanceToCenter;
            case Direction.Right: return xDistanceFromCellStart >= distanceToCenter;
            case Direction.Left: return xDistanceFromCellStart <= distanceToCenter;
        }

        return false;
    }
}
