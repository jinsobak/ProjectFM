using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    BuildingArea buildingArea;
    [SerializeField]
    MainBuilding mainBuilding;

    public void InitBoard()
    {
        buildingArea.Init_buildingArea(5);
        mainBuilding.Init_mainBuilding();
    }



}
