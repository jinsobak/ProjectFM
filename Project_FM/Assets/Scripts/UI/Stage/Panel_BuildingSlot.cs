using System.Collections.Generic;
using UnityEngine;

public class Panel_BuildingSlot : UI_Panel
{
    [SerializeField]
    private GameObject buildingSlotPF;
    [SerializeField] 
    private Transform slotParent;
    [SerializeField]
    private List<UI_Slot_Building> slots;

    public void InitUIWithData(List<BuildingData> deck)
    {
        // 슬롯 오브젝트 전체 파괴 후 슬롯을 null로 초기화
        if (slots != null)
        {
            foreach (UI_Slot_Building slot in slots)
            {
                if(slot != null)
                    Destroy(slot.gameObject);
            }
            slots.Clear();
        }

        // 덱 길이를 구하고 새 덱 슬롯 배열 생성
        int deckLength = deck.Count;
        slots = new List<UI_Slot_Building>();

        // 덱 길이 만큼 건물 슬롯 생성 및 초기화, 슬롯 리스트에 슬롯 추가
        for (int i = 0; i < deckLength; i++)
        {
            GameObject slotObject = Instantiate(buildingSlotPF, parent: slotParent);
            UI_Slot_Building slotCP = slotObject.GetComponent<UI_Slot_Building>();
            slotCP.InitSlot(deck[i]);

            slots.Add(slotCP);
        }
    }

}
