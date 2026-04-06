using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MockUserData
{
    public List<UnitData> availableUnits = new List<UnitData>();

    public UnitData[] deck = new UnitData[4];
    public int deckSlotCount = 4;

    public int mainBuildingHp = 100;
    public int waterProduceAmount = 1;
    public int buildingSlotCount = 2;

}
