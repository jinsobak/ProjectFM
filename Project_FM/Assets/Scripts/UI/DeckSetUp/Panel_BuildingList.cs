using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Panel_BuildingList : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_availableBuiling_PF;
    [SerializeField]
    private Transform slotParent;

    private List<BuildingData> availableBuildingList;
    private List<Slot_AvailableBuilding> slots_availableBuilding = new List<Slot_AvailableBuilding>();

    private UI_Stage_DeckSetUP masterUI;

    public void Init(UI_Stage_DeckSetUP masterUI, List<BuildingData> availableBuildingList)
    {
        this.masterUI = masterUI;

        // MockUserdata에서 사용 가능한 건물 리스트를 불러와 새 리스트로 저장
        this.availableBuildingList = availableBuildingList;

        // 이미 생성되어 있던 슬롯 파괴 및 슬롯 리스트 초기화
        foreach(Slot_AvailableBuilding slot in slots_availableBuilding) 
        {
            Destroy(slot.gameObject);
        }
        slots_availableBuilding.Clear();

        // 리스트를 순회하며 건물 슬롯 생성 및 초기화
        foreach (BuildingData buildingData in availableBuildingList )
        {
            GameObject object_slot_availableBuilding = Instantiate(slot_availableBuiling_PF, slotParent);
            Slot_AvailableBuilding CP_slot_availableBuilding = object_slot_availableBuilding.GetComponent<Slot_AvailableBuilding>();
            CP_slot_availableBuilding.InitSlot(buildingData, masterUI);

            // 슬롯 리스트에 생성된 슬롯 저장
            slots_availableBuilding.Add(CP_slot_availableBuilding);
        }


        RefreshSlots();
    }

    public void RefreshSlots()
    {
        BuildingData[] deck_inStage = masterUI.deck_inStage;

        for(int i = 0; i < slots_availableBuilding.Count; i++)
        {
            Slot_AvailableBuilding slot = slots_availableBuilding[i];

            bool isEquiped = false;

            for(int j = 0; j < deck_inStage.Length; j++)
            {
                if (deck_inStage[j] != null && deck_inStage[j].buildingName == slot.buildingData.buildingName)
                {
                    isEquiped = true;
                    break;
                }
            }

            slot.SetEquipState(isEquiped);
        }
    }
}
