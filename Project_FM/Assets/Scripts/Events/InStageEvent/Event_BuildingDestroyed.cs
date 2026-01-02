using System.Collections.Generic;
using UnityEngine;

public class Event_BuildingDestroyed
{
    public readonly List<UnitData> unitDatas;

    public Event_BuildingDestroyed(List<UnitData> _unitDatas = null)
    {
        unitDatas = _unitDatas;
    }
}
