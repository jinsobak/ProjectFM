using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    BuildingArea buildingArea;      // Player Building Area
    [SerializeField]
    MainBuilding mainBuilding;      // Player Main Building

    public void Init()
    {
        buildingArea.Init(5);
        mainBuilding.Init();
    }

}
