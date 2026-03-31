using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stage_DeckSetUP : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    private Panel_BuildingList panel_buildingList;
    [SerializeField]
    private Panel_Deck panel_deck;

    public BuildingData baseBuilding;
    public BuildingData[] deck_inStage;

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
        baseBuilding = StageManager.instance.mockUserData.baseBuilding;

        if(StageManager.instance.mockUserData.deck != null)
        {
            deck_inStage = StageManager.instance.mockUserData.deck;
        }
        else
        {
            deck_inStage = new BuildingData[StageManager.instance.mockUserData.deckSlotCount];
        }
        List<BuildingData> availableBuildingList = StageManager.instance.mockUserData.availableBuildings;

        panel_buildingList.Init(this, availableBuildingList);
        panel_deck.Init(this, deck_inStage);

        EnableUI();
    }

    public void TryEquipBuilding(BuildingData buildingData)
    {
        // 건물 장착 조건을 검사하여 조건 만족 시 장착 및 UI 초기화
        if(CheckEquipCondition(buildingData, out int index))
        {
            deck_inStage[index] = buildingData;
            StageManager.instance.mockUserData.deck = deck_inStage;
            RefreshAllUI();
        }
    }

    private bool CheckEquipCondition(BuildingData buildingData, out int index)
    {
        index = -1;

        for (int i = 0; i < deck_inStage.Length; i++)
        {
            if (deck_inStage[i] == buildingData)
            {
                return false;
            }
            else if (deck_inStage[i] == null)
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    public void TryUnEquipBuilding(BuildingData buildingData)
    {
        if(CheckUnEquipCondition(buildingData, out int index))
        {
            deck_inStage[index] = null;
            StageManager.instance.mockUserData.deck = deck_inStage;
            RefreshAllUI();
        }
    }

    private bool CheckUnEquipCondition(BuildingData buildingData, out int index)
    {
        index = -1;

        // 스테이지 내부 덱 배열을 순회해 장착 해제할 건물과 같은 건물이 있다면 true와 인덱스 반환
        for (int i = 0; i < deck_inStage.Length; i++)
        {
            if (deck_inStage[i] == buildingData)
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    private void RefreshAllUI()
    {
        panel_buildingList.RefreshSlots();
        panel_deck.RefreshSlots();
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
