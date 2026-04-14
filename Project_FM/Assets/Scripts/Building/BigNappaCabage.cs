using UnityEngine;

public class BigNappaCabage : Building
{
    public override void OnConstruct()
    {
        EventManager.Publish(new Event_BuildingConstructed(buildingData));
    }
}
