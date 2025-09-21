using UnityEngine;

public class GridObject
{
    private Vector2Int cellPosition;

    public Vector2Int GetCellPosition => cellPosition;
    public GridObjectType Type { get; set; } = GridObjectType.Empty;

    public GridObject(Vector2Int cellPosition)
    {
        this.cellPosition = cellPosition;
    }

    public override string ToString()
    {
        return "GridObject: " + cellPosition;
    }
}

public enum GridObjectType { Wall, Path, Empty }