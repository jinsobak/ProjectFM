using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Base,
    Sub,
}

public enum Region
{
    VegetableMeadow,
    FruitForest,

}

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public int buildingId;
    public string buildingName;
    public Sprite buildingIcon;

    public BuildingType buildingType;
    public Region buildingRegion;

    public List<Vector2Int> buildingPositions;
    public List<UnitData> producableUnitList;
}
