using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MockUserData
{
    public List<BuildingData> availableBuildings = new List<BuildingData>();

    public BuildingData baseBuilding;
    public BuildingData[] deck = new BuildingData[2];
    public int deckSlotCount = 2;

    public int mainBuildingHp = 100;
    public int waterProduceAmount = 1;
    public int buildingSlotCount = 2;

}
