using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Xml.Serialization;

public enum buildMode
{
    None,
    Construct,
    Destroy,
}

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public buildMode buildMode { get; private set; }

    private BuildingArea area;

    public GameObject selectedBuilding { get; private set; }

    private event Action OnDestroyModeEnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetArea(BuildingArea area)
    {
        this.area = area;
    }

    public void OnCellClicked(int index)
    {
        if (buildMode == buildMode.Construct && selectedBuilding != null)
        {
            Debug.Log("Try build building");
            area.BuildBuilding(selectedBuilding, index);
        }
    }

    public void SelectBuilding(GameObject newBuilding)
    {
        selectedBuilding = newBuilding;
    }

    public void ChangeBuildMode(buildMode buildMode)
    {
        this.buildMode = buildMode;

        switch (this.buildMode)
        {
            case buildMode.None:
                SelectBuilding(null);
                OnDestroyModeEnd?.Invoke();
                break;
        }
    }

    public void DestroyBuilding(GameObject building)
    {
        if (area != null)
        {
            area.DestroyBuilding(building);
            return;
        }
    }

    public void RegisterDestroyModeEnd(Action action)
    {
        OnDestroyModeEnd += action;
    }

    public void UnregisterDestroyModeEnd(Action action)
    {
        OnDestroyModeEnd -= action;
    }
}
