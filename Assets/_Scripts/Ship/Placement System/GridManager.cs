using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Grid grid;

    public Vector3Int GetGridPosition(Vector2 worldPos)
    {
        return grid.WorldToCell(worldPos);
    }

    public Vector2 AlignToGrid(Vector2 worldPos)
    {
        Vector3Int cellPos = GetGridPosition(worldPos);
        return grid.CellToWorld(cellPos);
    }
}
