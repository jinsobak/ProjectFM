using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUI : MonoBehaviour
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
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_BuildingConstructed>(AddSlot);
    }

    private void AddSlot(Event_BuildingConstructed message)
    {
        List<UnitData> unitDatas = message.unitDatas;

        foreach(UnitData data in unitDatas)
        {
            if(summonableUnits.Add(data))
            {
                SpawnSlotUI slotUI = Instantiate(spawnSlotPF, parent: transform).GetComponent<SpawnSlotUI>();
                slotUI.InitSlot(data);
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
}
