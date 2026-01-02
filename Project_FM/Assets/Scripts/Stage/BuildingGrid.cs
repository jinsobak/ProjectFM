using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class BuildingGrid : MonoBehaviour
{
    private Cell[,] cells;
    private Vector2 cellSize = Vector2.zero;
    public GameObject cellPrefab;
    private int gridWidth;
    private int gridHeight;

    public void InitGrid(int width, int height)
    {
        Debug.Log(string.Format("Grid width: {0}, height: {1}", width, height));
        gridWidth = width;
        gridHeight = height;

        cells = new Cell[width, height];
        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject newCellGO = Instantiate(cellPrefab, transform);
                Cell newCell = newCellGO.AddComponent<Cell>();
                newCell.Init(this, CellType.Empty, x, y);
                if (cellSize == Vector2.zero)
                    cellSize = newCell.transform.localScale;
                newCellGO.transform.localPosition = CalCellPos(width, height, x, y);

                cells[x, y] = newCell;
            }
        }
    }

    private Vector2 CalCellPos(int width, int height, int x, int y)
    {
        Vector2 newCellPos = new Vector2(0, 0);

        newCellPos.x = (x - (width / 2) + 0.6f * y) * cellSize.x;
        newCellPos.y = (y - (height / 2)) * cellSize.y * 0.85f;

        return newCellPos;
    }

    public void BuildBuilding(GameObject buildingPF, int posX, int posY)
    {
        Building buildingCP = buildingPF.GetComponent<Building>();

        if (!CanBuild(buildingCP.buildingPositions, posX, posY))
            return;

        Debug.Log("Build Building");

        GameObject newBuilding = Instantiate(buildingPF, transform);
        newBuilding.transform.localPosition = cells[posX, posY].transform.localPosition;
        Building newBuildingCP = newBuilding.GetComponent<Building>();
        newBuildingCP.SetPosition(posX, posY);
        newBuildingCP.OnConstruct();

        foreach (Vector2Int position in buildingCP.buildingPositions)
        {   
            cells[posX + position.x, posY + position.y].SetType(CellType.Constructed);
        }
    }

    private bool CanBuild(List<Vector2Int> buildingPositions, int cellPosX, int cellPosY)
    {
        foreach (Vector2Int position in buildingPositions)
        {
            if (cellPosX + position.x < 0 || cellPosX + position.x >= gridWidth)
            {
                return false;
            }
            if (cellPosY + position.y < 0 || cellPosY + position.y >= gridHeight)
            {
                return false;
            }

            if (cells[cellPosX + position.x, cellPosY + position.y].cellType == CellType.Constructed)
                return false;
        }

        return true;
    }

    public void DestroyBuilding(GameObject building)
    {
        Debug.Log("Destroy Building");

        Building buildingCP = building.GetComponent<Building>();
        List<Vector2Int> buildingPoses = buildingCP.buildingPositions;
        int x = buildingCP.x;
        int y = buildingCP.y;

        foreach(Vector2Int position in buildingPoses)
        {
            cells[x + position.x, y + position.y].SetType(CellType.Empty);
        }

        EventManager.Publish(new Event_BuildingDestroyed(buildingCP.buildingData.producableUnitList));
        Destroy(building);
    }
}
