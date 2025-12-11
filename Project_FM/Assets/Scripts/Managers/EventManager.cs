using System.Collections.Generic;
using System;
using UnityEngine;

public static class EventManager
{
    public static Action<List<UnitData>> OnBuildingConstructed;
    public static Action OnBuildingDestroyed;

    public static Dictionary<Type, Delegate> eventDict = new Dictionary<Type, Delegate>();
    
    public static void RegisterEvent<T>(Action<T> _action)
    {
        Type type = typeof(T);

        if(eventDict.ContainsKey(type))
        {
            Delegate currentEvent = eventDict[type];

            eventDict[type] = Delegate.Combine(currentEvent, _action);
        }
        else
        {
            eventDict.Add(type, _action);
        }
    }

    public static void UnRegisterEvent<T>(Action<T> _action)
    {
        Type type = typeof(T);

        if(eventDict.ContainsKey(type))
        {
            Delegate removeAction = eventDict[type];

            eventDict[type] = Delegate.Remove(removeAction, _action);
        }
    }

    public static void Publish<T>(T evnetMessage)
    {
        Type type = typeof(T);

        if(eventDict.ContainsKey(type))
        {
            (eventDict[type] as Action<T>)?.Invoke(evnetMessage);
        }
    }
}
