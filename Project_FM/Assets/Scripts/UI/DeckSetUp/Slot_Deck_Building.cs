using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Deck_Building : MonoBehaviour
{
    [SerializeField]
    private BuildingData buildingData;
    [SerializeField]
    private Image icon_building;
    [SerializeField]
    private GameObject slot_base;

    private UI_Stage_DeckSetUP masterUI;

    public void InitSlot(BuildingData _buildingData, UI_Stage_DeckSetUP masterUI)
    {
        this.masterUI = masterUI;

        if (_buildingData == null)
        {
            this.buildingData = null;
            if (icon_building != null)
            {
                icon_building.sprite = null;
            }
            if (slot_base != null)
            {
                slot_base.SetActive(false);
            }
        }
        else
        {
            // 건물 데이터 저장
            this.buildingData = _buildingData;
            // 아이콘 적용
            if (icon_building != null)
            {
                icon_building.sprite = _buildingData.buildingIcon;
            }
            if (slot_base != null)
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
    }

    /// <summary>
    /// 클릭 시 정보 UI에 정보를 출력하는 함수
    /// 기본적으로 마우스 좌클릭에 할당
    /// </summary>
    public void ShowInfo()
    {

    }
}
