using UnityEngine;

public class GridRenderer
{
    private Vector3 origin;

    public GridRenderer(Vector3 origin)
    {
        this.origin = origin;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) + origin;
    }
}
