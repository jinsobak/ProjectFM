using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUI : UI
{
    [SerializeField]
    private GameObject spawnSlotPF;
    [SerializeField]
    private Transform spawnPos;

    private HashSet<UnitData> summonableUnits;
    private List<SpawnSlotUI> slots;

    private void OnEnable()
    {
        if (summonableUnits == null)
            summonableUnits = new HashSet<UnitData>();
        if(slots == null)
            slots = new List<SpawnSlotUI>();

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
                SpawnSlotUI slotUI = Instantiate(spawnSlotPF, parent: transform).GetComponent<SpawnSlotUI>();
                slotUI.InitSlot(data);
                slotUI.action_slotDestroy -= OnSlotDestroyed;
                slotUI.action_slotDestroy += OnSlotDestroyed;
                slots.Add(slotUI);
            }
            else
            {
                if(FindSlot(data, out SpawnSlotUI slot))
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
            if(FindSlot(data, out SpawnSlotUI slot))
            {
                slot.SubstractStack();
            }
        }
    }

    private bool FindSlot(UnitData data, out SpawnSlotUI slot)
    {
        slot = null;

        foreach (SpawnSlotUI slotUI in slots)
        {
            if(slotUI.unitData.name == data.name)
            {
                slot = slotUI;
                return true;
            }
        }

        return false;
    }

    private void OnSlotDestroyed(SpawnSlotUI slot)
    {
        slots.Remove(slot);
        summonableUnits.Remove(slot.unitData);
    }
}
