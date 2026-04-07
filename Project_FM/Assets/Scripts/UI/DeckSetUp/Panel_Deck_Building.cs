using System.Collections.Generic;
using UnityEngine;

public class Panel_Deck_Building : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_deckPF;
    [SerializeField]
    private Transform slotParent;

    private List<Slot_Deck_Building> slots_deck;
    private UI_Stage_DeckSetUP masterUI;

    /// <summary>
    /// 패널 초기화
    /// </summary>
    /// <param name="masterUI">최상위 클래스</param>
    /// <param name="initalBuildingDeckData">초기 건물 덱 데이터 리스트</param>
    public void Init(UI_Stage_DeckSetUP masterUI, List<BuildingData> initalBuildingDeckData)
    {
        this.masterUI = masterUI;

        // 덱 슬롯 개수
        int deckCount = masterUI.deckSlotCount;
        // 덱 슬롯 최대 개수
        int maxDeckCount = masterUI.maxDeckSlotCount;

        // 슬롯 관리용 리스트
        slots_deck = new List<Slot_Deck_Building>();

        for (int i = 0; i < maxDeckCount; i++)
        {
            GameObject object_slot_deck = Instantiate(slot_deckPF, slotParent);
            Slot_Deck_Building cp_slot_deck = object_slot_deck.GetComponent<Slot_Deck_Building>();

            if (i < initalBuildingDeckData.Count)
            {
                cp_slot_deck.InitSlot(initalBuildingDeckData[i], masterUI);
            }
            else
            {
                cp_slot_deck.InitSlot(null, masterUI);
            }

            slots_deck.Add(cp_slot_deck);
        }
    }

    /// <summary>
    /// 덱 슬롯 전체 초기화
    /// </summary>
    public void RefreshSlots()
    {
        // 최상위 클래스에서 건물 덱 리스트 참조
        List<BuildingData> deckData = masterUI.deck_building_inStage;
        // 슬롯 총 개수 저장
        int deckCount = slots_deck.Count;

        // 전체 슬롯을 순회하며 초기화 
        for (int i = 0; i < deckCount; i++)
        {
            // 건물 데이터 리스트 길이 만큼 슬롯에 덱 데이터 전달
            if(i < deckData.Count)
            {
                slots_deck[i].InitSlot(deckData[i], masterUI);
            }
            else    // 나머지 슬롯에는 null 전달
            {
                slots_deck[i].InitSlot(null, masterUI);
            }
        }
    }
}
