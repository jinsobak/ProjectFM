using System.Collections.Generic;
using UnityEngine;

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
    public GameObject buildingPF;

    public Region buildingRegion;

    public int cost;
}
