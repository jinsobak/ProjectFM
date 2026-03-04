using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_UnitSpawnSlot : UI
{
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject spawnSlotPF;

    private HashSet<UnitData> summonableUnits;
    private List<Slot_UnitSpawn> slots;

    private void OnEnable()
    {
        if (summonableUnits == null)
            summonableUnits = new HashSet<UnitData>();
        if(slots == null)
            slots = new List<Slot_UnitSpawn>();

        EventManager.RegisterEvent<Event_BuildingConstructed>(AddSlot);
        EventManager.RegisterEvent<Event_BuildingDestroyed>(TakeOutSlot);
    }

    private void OnDestroy()
    {
        //EventManager.UnRegisterEvent<Event_BuildingConstructed>(AddSlot);
    }

    private void AddSlot(Event_BuildingConstructed message)
    {
        Debug.Log("Add Slot");

        if (message.unitDatas == null)
            return;

        List<UnitData> unitDatas = message.unitDatas;

        foreach(UnitData data in unitDatas)
        {
            if(summonableUnits.Add(data))
            {
                Slot_UnitSpawn slotUI = Instantiate(spawnSlotPF, parent: panel).GetComponent<Slot_UnitSpawn>();
                slotUI.InitSlot(data);
                slotUI.action_slotDestroy -= OnSlotDestroyed;
                slotUI.action_slotDestroy += OnSlotDestroyed;
                slots.Add(slotUI);
            }
            else
            {
                if(FindSlot(data, out Slot_UnitSpawn slot))
                {
                    slot.AddStack();
                }
            }
        }        
    }

    private void TakeOutSlot(Event_BuildingDestroyed message)
    {
        Debug.Log("Take Out Slot");

        if (message.unitDatas == null)
            return;

        List<UnitData> unitDatas = message.unitDatas;

        foreach(UnitData data in unitDatas)
        {
            if(FindSlot(data, out Slot_UnitSpawn slot))
            {
                slot.SubstractStack();
            }
        }
    }

    private bool FindSlot(UnitData data, out Slot_UnitSpawn slot)
    {
        slot = null;

        foreach (Slot_UnitSpawn slotUI in slots)
        {
            if(slotUI.unitData.name == data.name)
            {
                slot = slotUI;
                return true;
            }
        }

        return false;
    }

    private void OnSlotDestroyed(Slot_UnitSpawn slot)
    {
        slots.Remove(slot);
        summonableUnits.Remove(slot.unitData);
    }
}
