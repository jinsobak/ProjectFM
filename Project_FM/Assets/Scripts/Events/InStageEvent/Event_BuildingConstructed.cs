using System.Collections.Generic;
using UnityEngine;

public struct Event_BuildingConstructed
{
    public readonly List<UnitData> unitDatas;

    public Event_BuildingConstructed(List<UnitData> _unitDatas)
    {
        unitDatas = _unitDatas;
    }
}
