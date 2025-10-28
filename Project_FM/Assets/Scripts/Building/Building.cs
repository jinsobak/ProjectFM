using UnityEngine;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour, IInteractable
{
    public string buildingName;
    public List<Vector2Int> buildingPositions;
    public BuildingData buildingData;

    public int x { get; private set; } = 0;
    public int y { get; private set; } = 0;

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

    public abstract void OnConstruct();
}
