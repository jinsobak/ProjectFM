using System.Collections.Generic;
using System;
using UnityEngine;

public enum StageState
{
    
}


public static class EventManager
{
    public static Action<List<UnitData>> OnBuildingConstructed;
    public static Action OnBuildingDestroyed;

    
}
