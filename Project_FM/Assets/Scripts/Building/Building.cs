using UnityEngine;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour, IInteractable
{
    public string buildingName;
    public BuildingData buildingData;

    public int x { get; private set; } = 0;
    public int y { get; private set; } = 0;
    public int slotIndex { get; private set; } = -1;

    public virtual void Interact()
    {
        Debug.Log("Building Interact");

        if(BuildManager.instance.buildMode == buildMode.Destroy)
        {
            BuildManager.instance.DestroyBuilding(gameObject);
        }
    }

    public virtual void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public virtual void SetIndex(int _index)
    {
        slotIndex = _index;
    }

    public abstract void OnConstruct();
}
