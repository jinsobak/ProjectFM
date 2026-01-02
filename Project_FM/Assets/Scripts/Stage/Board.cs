using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int gridWidth = 4;
    [SerializeField]
    private int gridHeight = 4;

    [SerializeField]
    private Transform gridPos;
    [SerializeField]
    private BuildingGrid grid;

    public void InitBoard()
    {
        grid.InitGrid(gridWidth, gridHeight);
    }

    public BuildingGrid GetBoard()
    {
        return grid;
    }
}
