using System;
using UnityEngine;

public class TestTree : Building
{

    private void Update()
    {
        
    }

    public override void OnConstruct()
    {
        EventManager.Publish(new Event_BuildingConstructed(buildingData.producableUnitList));
    }
}
