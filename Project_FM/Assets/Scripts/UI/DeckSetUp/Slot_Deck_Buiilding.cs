using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Deck_Buiilding : UI, IPointerClickHandler
{
    [SerializeField]
    private BuildingData buildingData;
    [SerializeField]
    private Image icon_building;
    [SerializeField]
    private GameObject slot_base;

    private UI_Stage_DeckSetUP masterUI;

    public void InitSlot(BuildingData buildingData, UI_Stage_DeckSetUP masterUI)
    {
        this.masterUI = masterUI;

        if(buildingData == null)
        {
            this.buildingData = null;
            if(icon_building != null)
            {
                icon_building.sprite = null;
            }
            if(slot_base != null)
            {
                slot_base.SetActive(false);
            }
        }
        else
        {
            // 건물 데이터 저장
            this.buildingData = buildingData;
            // 아이콘 적용
            if(icon_building != null)
            {
                icon_building.sprite = buildingData.buildingIcon;
            }
            if(slot_base != null)
            {
                slot_base.SetActive(true);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowInfo();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnEquip();
        }
    }

    /// <summary>
    /// 클릭 시 정보 UI에 정보를 출력하는 함수
    /// 기본적으로 마우스 좌클릭에 할당
    /// </summary>
    public void ShowInfo()
    {

    }

    /// <summary>
    /// 덱에 건물을 추가하는 함수 
    /// 기본적으로 마우스 우클릭에 할당
    /// </summary>
    public void UnEquip()
    {
        // 건물 데이터나 최상위 클래스가 없다면 즉시 리턴
        if (buildingData == null || masterUI == null)
        {
            return;
        }

        // 최상위 클래스에 건물 해제 시도 요청
        //masterUI.TryUnEquipBuilding(buildingData);
    }
}
