using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Stage_DeckSetUP : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    private Panel_UnitList panel_unitList;
    [SerializeField]
    private Panel_Deck_Unit panel_deck;
    [SerializeField]
    private Panel_Deck_Building panel_deck_building;
    
    public int maxDeckSlotCount;
    public int deckSlotCount;

    public UnitData[] deck_inStage;
    public List<BuildingData> deck_building_inStage;

    public void Awake()
    {
        DisableUI();
        EventManager.RegisterEvent<Event_InStage_SetDeckStart>(Init);
    }

    public void OnDestroy()
    {
        EventManager.UnRegisterEvent<Event_InStage_SetDeckStart>(Init);
    }

    public void Init(Event_InStage_SetDeckStart message)
    {
        maxDeckSlotCount = StageManager.instance.mockUserData.maxDeckSlotCount;
        deckSlotCount = StageManager.instance.mockUserData.deckSlotCount;

        // 임시 유저 데이터에서 덱 관련 데이터 불러옴
        // 덱 데이터 불러옴
        if(StageManager.instance.mockUserData.deck != null)
        {
            deck_inStage = StageManager.instance.mockUserData.deck.ToArray();
        }
        else
        {
            deck_inStage = new UnitData[StageManager.instance.mockUserData.deckSlotCount];
        }
        // 사용 가능 유닛 리스트 불러옴
        List<UnitData> availableUnitList = StageManager.instance.mockUserData.availableUnits;
        // 불러온 덱 데이터를 사용 해 건물 덱 업데이트
        UpdateBuildingDeck();

        // UI 초기화
        panel_unitList.Init(this, availableUnitList);
        panel_deck.Init(this, deck_inStage);
        panel_deck_building.Init(this, deck_building_inStage);

        EnableUI();
    }

    public void TryEquipUnit(UnitData unitData)
    {
        // 건물 장착 조건을 검사하여 조건 만족 시 장착 및 UI 초기화
        if(CheckEquipCondition(unitData, out int index))
        {
            deck_inStage[index] = unitData;
            // 건물 덱 데이터 업데이트
            UpdateBuildingDeck();
            // 전체 UI 다시 로드
            RefreshAllUI();
        }
    }

    /// <summary>
    /// 유닛 장착 조건 확인용 함수
    /// 조건 만족 시 빈 슬롯의 인덱스와 함께 true 반환
    /// </summary>
    /// <param name="unitData"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool CheckEquipCondition(UnitData unitData, out int index)
    {
        // 빈 슬롯의 인덱스
        index = -1;
        // 빈 슬롯이 있는지 확인용 변수
        bool hasEmptySlot = false;

        // 덱 배열을 순회하며 빈 자리가 있다면 
        // 인덱스 저장 후 다음 조건 확인으로 넘어감
        for (int i = 0; i < deck_inStage.Length; i++)
        {
            if (deck_inStage[i] == unitData)
            {
                return false;
            }
            else if (deck_inStage[i] == null)
            {
                index = i;
                hasEmptySlot = true;
                break;
            }
        }

        // 덱에 빈 자리가 없다면 false 반환
        if (!hasEmptySlot)
            return false;

        // HashSet을 이용해 임시 건물 세트 생성 및 기존 덱의 필요 건물을 세트에 저장
        HashSet<BuildingData> tempBuildingSet = new HashSet<BuildingData>();
        foreach (UnitData data in deck_inStage)
        {
            if (data != null && data.requireBuilding != null)
                tempBuildingSet.Add(data.requireBuilding);
        }
        // 빈 자리에 넣을 유닛의 필요 건물을 세트에 추가
        if(unitData.requireBuilding != null)
        {
            tempBuildingSet.Add(unitData.requireBuilding);
        }

        // 건물 슬롯 길이보다 임시 건물 세트 길이가 더 크다면 false 반환
        if (tempBuildingSet.Count > deckSlotCount)
            return false;

        return true;
    }

    /// <summary>
    /// 유닛 장착 해제 시도 함수
    /// </summary>
    /// <param name="unitData"></param>
    public void TryUnEquipUnit(UnitData unitData)
    {
        if(CheckUnEquipCondition(unitData, out int index))
        {
            deck_inStage[index] = null;
            UpdateBuildingDeck();
            RefreshAllUI();
        }
    }

    /// <summary>
    /// 유닛 장착 해제 조건 확인용 함수
    /// </summary>
    /// <param name="unitData"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool CheckUnEquipCondition(UnitData unitData, out int index)
    {
        index = -1;

        // 스테이지 내부 덱 배열을 순회해 장착 해제할 건물과 같은 건물이 있다면 true와 인덱스 반환
        for (int i = 0; i < deck_inStage.Length; i++)
        {
            if (deck_inStage[i] == unitData)
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    private void UpdateBuildingDeck()
    {
        HashSet<BuildingData> tempBuildingSet = new HashSet<BuildingData>();

        foreach(UnitData unitData in deck_inStage)
        {
            if(unitData != null && unitData.requireBuilding != null)
            {
                tempBuildingSet.Add(unitData.requireBuilding);
            }
        }

        deck_building_inStage = tempBuildingSet.ToList();
    }

    private void RefreshAllUI()
    {
        panel_unitList.RefreshSlots();
        panel_deck.RefreshSlots();
        panel_deck_building.RefreshSlots();
    }

    private void EnableUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
