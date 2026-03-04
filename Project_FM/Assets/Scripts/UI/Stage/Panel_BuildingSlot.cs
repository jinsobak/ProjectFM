using System.Collections.Generic;
using UnityEngine;

public class Panel_BuildingSlot : UI
{
    [SerializeField]
    private GameObject buildingSlotPF;
    [SerializeField]
    private List<Slot_Building> slots;

    private void OnEnable()
    {
        if (slots == null)
            slots = new List<Slot_Building>();
    }

    private void OnDisable()
    {

    }


}
