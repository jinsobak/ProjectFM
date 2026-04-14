using System.Collections.Generic;
using UnityEngine;

public class Event_BuildingDestroyed
{
    public readonly BuildingData buildingData;

    public Event_BuildingDestroyed(BuildingData buildingData)
    {
        this.buildingData = buildingData;
    }
}
