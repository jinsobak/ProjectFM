using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_AvailableUnit : UI, IPointerClickHandler
{
    [SerializeField]
    public UnitData unitData;
    [SerializeField]
    private Image image_disable;
    [SerializeField]
    private Image icon_building;

    private UI_Stage_DeckSetUP masterUI;
    private bool disabled = false;

    public void InitSlot(UnitData unitData, UI_Stage_DeckSetUP masterUI)
    {
        this.masterUI = masterUI;
        // 건물 정보 저장
        this.unitData = unitData;

        if (unitData != null && unitData.unitIcon != null)
        {
            // 아이콘 적용
            icon_building.sprite = unitData.unitIcon;
        }

        // 비활성화 이미지 비활성화
        image_disable.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowInfo();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Equip();
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
    public void Equip()
    {
        // 건물 데이터나 최상위 클래스가 없다면 즉시 리턴
        if (unitData == null || masterUI == null)
        {
            return;
        }

        // 이미 장착되어 있다면 즉시 리턴
        if (disabled)
        {
            Debug.Log("이미 장착된 건물입니다.");
            return;
        }

        // 최상위 클래스에 건물 장착 시도 요청
        masterUI.TryEquipBuilding(unitData);
    }

    public void SetEquipState(bool state)
    {
        disabled = state;

        if (image_disable != null)
        {
            // 건물이 장착되어 있다면 활성화, 아니면 비활성화
            image_disable.gameObject.SetActive(disabled);
        }
    }
}
