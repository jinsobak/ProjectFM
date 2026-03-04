using UnityEngine;
using System.Collections.Generic;

public class Panel_SlotTabGroup : UI
{
    //UI탭들을 저장할 리스트
    [SerializeField]
    private List<UI> panelList = new List<UI>();

    //현재 탭 인덱스
    private int tapIndex = 0;

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_SlotTapChange>(ChangeTap);
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_InStage_SlotTapChange>(ChangeTap);
    }

    private void Start()
    {
        InitUI();
    }

    public override void InitUI()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if(i == 0)
            {
                panelList[i].gameObject.SetActive(true);
            }
            else
            {
                panelList[i].gameObject.SetActive(false);
            }
        }
    }

    private void ChangeTap(Event_InStage_SlotTapChange tapChangeEvent)
    {
        tapIndex = tapIndex < panelList?.Count - 1 ? tapIndex + 1 : 0;

        for(int i = 0; i < panelList?.Count; i++)
        {
            if (i == tapIndex)
            {
                panelList[i].gameObject.SetActive(true);
            }
            else
            {
                panelList[i].gameObject.SetActive(false);
            }
        }
    }


}
