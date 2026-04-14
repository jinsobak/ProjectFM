using System.Collections.Generic;
using UnityEngine;

public struct Event_BuildingConstructed
{
    public readonly BuildingData buildingData;

    public Event_BuildingConstructed(BuildingData buildingData)
    {
        this.buildingData = buildingData;
    }
}
