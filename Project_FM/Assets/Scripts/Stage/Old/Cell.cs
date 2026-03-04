using UnityEngine;

public enum CellType
{
    None = 0,
    Empty = 1,
    Constructed = 2,
}


public class Cell : MonoBehaviour, IInteractable
{
    public CellType cellType { get; private set; } = CellType.None;

    private BuildingGrid grid;

    public int x { get; private set; }
    public int y { get; private set; }

    public void Init(BuildingGrid grid, CellType type, int x, int y)
    {
        this.grid = grid;
        cellType = type;
        SetCoord(x, y);
    }

    private void SetCoord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetType(CellType type)
    {
        cellType = type;
    }

    public void Interact()
    {
        BuildManager.instance.OnCellClicked(x, y);
    }
}
