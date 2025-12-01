using UnityEngine;

public partial class GridManager
{
    public float GetGridSize() => gridRenderer.GridSize;

    public Vector3 GetCurrentCellPosition(Vector3 currentPos)
    {
        GridCell cell = gridRenderer.GetCellFromWorldPosition(currentPos);
        return gridRenderer.GetCellCenter(cell);
    }

    public bool HasReachedCellCenterInMoveDirection(Vector2 dir, Vector3 currentPos)
    {
        return gridRenderer.HasReachedCellCenterInMoveDirection(dir, currentPos);
    }

    public bool IsNeigbourCellWalkable(Vector3 currentPosition, Vector2 direction)
    {
        GridCell neighbourCell = GetNeighbourCell(currentPosition, direction);
        GridObject gridObject = grid.GetGridObject(neighbourCell);

        return gridObject.Type == GridObjectType.Path;
    }

    public Vector3 GetNeighbourPositionFromDirection(Vector3 currentPos, Vector2 direction)
    {
        GridCell neighbourCell = GetNeighbourCell(currentPos, direction);

        return gridRenderer.GetCellCenter(neighbourCell);
    }

    public GridCell GetNeighbourCell(Vector3 currentPos, Vector2 direction)
    {
        GridCell currentCell = gridRenderer.GetCellFromWorldPosition(currentPos);
        Direction dir = gridRenderer.GetDirectionFromVector(direction);

        return grid.GetNeighbourCell(currentCell, dir);
    }
}
