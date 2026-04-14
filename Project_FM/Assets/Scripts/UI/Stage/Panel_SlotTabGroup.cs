using UnityEngine;
using System.Collections.Generic;

public class Panel_SlotTabGroup : UI_Panel
{
    //UI탭들을 저장할 리스트
    private List<UI_Panel> panelList = new List<UI_Panel>();

    [SerializeField]
    private Panel_BuildingSlot panel_slots_building;
    [SerializeField]
    private Panel_Slots_UnitSpawn panel_slots_unitSpawn;

    //현재 탭 인덱스
    private int tapIndex = 0;

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_SlotTapChange>(ChangeTap);
        EventManager.RegisterEvent<Event_InStage_StartCountDown>(StartInitUI);
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_InStage_SlotTapChange>(ChangeTap);
        EventManager.UnRegisterEvent<Event_InStage_StartCountDown>(StartInitUI);
    }

    private void StartInitUI(Event_InStage_StartCountDown message)
    {
        // 유닛 덱과 건물 덱 데이터 불러옴
        UnitData[] deck = StageManager.instance.mockUserData.deck;
        List<BuildingData> deck_building = StageManager.instance.mockUserData.deck_building;

        // 패널 리스트에 건물 슬롯 패널 추가
        // 건물 슬롯 패널에 건물 덱 전달 및 초기화
        if(panel_slots_building != null && deck_building != null) 
        {
            panelList.Add(panel_slots_building);
            panel_slots_building.InitUIWithData(deck_building);
        }
        // 패널 리스트에 유닛 슬롯 패널 추가
        // 유닛 슬롯 패널에 유닛 덱 전달 및 초기화
        if(panel_slots_unitSpawn != null && deck != null) 
        {
            panelList.Add(panel_slots_unitSpawn);
            panel_slots_unitSpawn.InitUIWithData(deck);
        }

        // 패널 초기화
        InitUI();
    }

    public override void InitUI()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if(i == 0)
            {
                panelList[i].EnableUI();
            }
            else
            {
                panelList[i].DisableUI();
            }

            panelList[i].InitUI();
        }
    }

    private void ChangeTap(Event_InStage_SlotTapChange tapChangeEvent)
    {
        tapIndex = tapIndex < panelList?.Count - 1 ? tapIndex + 1 : 0;

        for(int i = 0; i < panelList?.Count; i++)
        {
            if (i == tapIndex)
            {
                panelList[i].EnableUI();
            }
            else
            {
                panelList[i].DisableUI();
            }
        }
    }


}
