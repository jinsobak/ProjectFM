using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

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

            //현재 등록된 델리게이트 리스트를 가져옴
            Delegate[] invocationList = currentEvent.GetInvocationList();

            //_action이 이미 리스트에 존재하는지 확인.
            bool isDuplicate = false;
            foreach (var d in invocationList)
            {
                if (d == (Delegate)_action)
                {
                    isDuplicate = true;
                    break;
                }
            }

            // 중복되지 않았을 때만 체이닝을 진행.
            if (!isDuplicate)
            {
                eventDict[type] = Delegate.Combine(currentEvent, _action);
            }
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
