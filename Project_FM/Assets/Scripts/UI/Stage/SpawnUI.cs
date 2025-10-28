using System.Collections.Generic;
using UnityEngine;

public class SpawnUI : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnSlotPF;

    private HashSet<UnitData> summonableUnits;
    private List<SpawnSlotUI> slots;

    private void OnEnable()
    {
        if (summonableUnits == null)
            summonableUnits = new HashSet<UnitData>();
        if(slots == null)
            slots = new List<SpawnSlotUI>();

        EventManager.OnBuildingConstructed += AddSlot;
    }

    private void OnDisable()
    {
        EventManager.OnBuildingConstructed -= AddSlot;
    }

    private void AddSlot(List<UnitData> unitDatas)
    {
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

            }
        }        
    }
}
