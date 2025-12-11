using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public List<Vector2Int> buildingPositions;

    public List<UnitData> producableUnitList;
}
