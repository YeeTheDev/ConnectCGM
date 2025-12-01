using UnityEngine;

public class GridObject
{
    private GridCell cellPosition;

    public GridCell GetCellPosition => cellPosition;
    public GridObjectType Type { get; set; } = GridObjectType.Empty;

    public GridObject(GridCell cellPosition)
    {
        this.cellPosition = cellPosition;
    }

    public override string ToString()
    {
        return "GridObject: " + cellPosition;
    }
}

public enum GridObjectType { Wall, Path, Empty }