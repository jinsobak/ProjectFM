using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot_Building : MonoBehaviour
{
    [Header("SlotData")]
    [SerializeField]
    private Image image_building;
    [SerializeField]
    private Image image_locked;
    [SerializeField]
    private TextMeshProUGUI text_Key;

    private BuildingData buildingData;
    private string key;
    private bool activated;

    private void OnDestroy()
    {
        ResourceManager.instance.UnRegisterEvent_WaterChanged(CheckBuildingCost);
    }

    public void InitSlot(BuildingData buildingData)
    {
        this.buildingData = buildingData;

        // 기본적으로 슬롯은 비활성화 상태
        DisableSlot();

        // 건물 데이터가 null일 경우
        // 건물 이미지 오브젝트 비활성화
        // 이후 즉시 반환
        if (buildingData == null)
        {
            image_building.gameObject.SetActive(false);
            return;
        }

        // 건물 데이터에 건물 이미지가 있다면
        // 건물 이미지 등록 및 건물 이미지 오브젝트 활성화
        // 아니라면 건물 이미지 비활성화
        if (buildingData.buildingIcon != null)
        {
            image_building.gameObject.SetActive(true);
            image_building.sprite = buildingData.buildingIcon;
        }
        else
        {
            image_building.gameObject.SetActive(false);
        }

        // 자원 수량 변경 이벤트에 등록
        ResourceManager.instance.RegisterEvent_WaterChanged(CheckBuildingCost);
        CheckBuildingCost();
    }    

    /// <summary>
    /// 클릭 시 BuildManager에 bulilingData의 건물 프리팹 전달 및 건설 모드를 "건설"로 변환
    /// </summary>
    public void OnClick()
    {
        if(buildingData == null)
        {
            Debug.Log("빈 건물 슬롯입니다.");
        }

        if(!activated)
        {
            Debug.Log("자원이 부족합니다. 코스트:" + buildingData.cost + " 건물 이름:" + buildingData.buildingName);
            return;
        }

        if(buildingData.buildingPF == null)
        {
            Debug.Log("건물 데이터 내 프리팹이 없습니다.");
            return;
        }

        Debug.Log("Building Selected");
        BuildManager.instance.ChangeBuildMode(buildMode.Construct);
        BuildManager.instance.SelectBuilding(buildingData.buildingPF);
    }

    public void EnableSlot()
    {
        Debug.Log("EnableSLot" + gameObject.name);
        activated = true;
        ResourceManager.instance.RegisterEvent_WaterChanged(CheckBuildingCost);
        image_locked.fillAmount = 0;
    }

    public void DisableSlot()
    {
        activated = false;
        ResourceManager.instance.UnRegisterEvent_WaterChanged(CheckBuildingCost);
        image_locked.fillAmount = 1;
    }

    private void CheckBuildingCost()
    {
        Debug.Log("CheckBuildingCost: " + gameObject.name);
        // 현재 자원이 건설 코스트보다 처음 커지면 슬롯 활성화
        bool alreadyActivated = activated;
        activated = ResourceManager.instance.water >= buildingData.cost;
        Debug.Log(alreadyActivated + " " + activated + " " + ResourceManager.instance.water);
        if(activated && !alreadyActivated)
        {
            Debug.Log("Enable");
            EnableSlot();
        }
    }
}
