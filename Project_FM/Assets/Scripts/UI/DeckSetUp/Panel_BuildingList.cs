using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Panel_BuildingList : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_availableUnit_PF;
    [SerializeField]
    private Transform slotParent;

    private List<UnitData> availableUnitList;
    private List<Slot_AvailableUnit> slots_availableUnit = new List<Slot_AvailableUnit>();

    private UI_Stage_DeckSetUP masterUI;

    public void Init(UI_Stage_DeckSetUP masterUI, List<UnitData> availableUnitList)
    {
        this.masterUI = masterUI;

        // MockUserdata에서 사용 가능한 건물 리스트를 불러와 새 리스트로 저장
        this.availableUnitList = availableUnitList;

        // 이미 생성되어 있던 슬롯 파괴 및 슬롯 리스트 초기화
        foreach(Slot_AvailableUnit slot in slots_availableUnit) 
        {
            Destroy(slot.gameObject);
        }
        slots_availableUnit.Clear();

        // 리스트를 순회하며 건물 슬롯 생성 및 초기화
        foreach (UnitData unitData in availableUnitList )
        {
            GameObject object_slot_availableUnit = Instantiate(slot_availableUnit_PF, slotParent);
            Slot_AvailableUnit CP_slot_availableUnit = object_slot_availableUnit.GetComponent<Slot_AvailableUnit>();
            CP_slot_availableUnit.InitSlot(unitData, masterUI);

            // 슬롯 리스트에 생성된 슬롯 저장
            slots_availableUnit.Add(CP_slot_availableUnit);
        }


        RefreshSlots();
    }

    public void RefreshSlots()
    {
        UnitData[] deck_inStage = masterUI.deck_inStage;

        for(int i = 0; i < slots_availableUnit.Count; i++)
        {
            Slot_AvailableUnit slot = slots_availableUnit[i];

            bool isEquiped = false;

            for(int j = 0; j < deck_inStage.Length; j++)
            {
                if (deck_inStage[j] != null && deck_inStage[j].unitName == slot.unitData.unitName)
                {
                    isEquiped = true;
                    break;
                }
            }

            slot.SetEquipState(isEquiped);
        }
    }
}
