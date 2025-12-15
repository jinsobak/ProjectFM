using System.Collections.Generic;
using UnityEngine;

public class BuildUI : UI
{
    [SerializeField]
    private GameObject buildingSlotPF;
    [SerializeField]
    private List<BuildingSlot> slots;

    private void OnEnable()
    {
        if (slots == null)
            slots = new List<BuildingSlot>();
    }

    private void OnDisable()
    {

    }
}
