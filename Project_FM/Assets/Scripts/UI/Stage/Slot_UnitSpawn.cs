using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UnitSpawn : MonoBehaviour
{
    [Header("SlotData")]
    [SerializeField]
    private Image image_locked;
    [SerializeField]
    private Image cooltimeImage;
    [SerializeField]
    private Image unitImageCP;
    [SerializeField]
    private TextMeshProUGUI keyText;
    [SerializeField]
    private TextMeshProUGUI stackText;

    private UnitData unitData;
    private int stack;
    private string key;

    [Header("UnitSpawnData")]
    private int curStack;
    private TimeData cooltimeData;
    private bool locked;

    private void Update()
    {
        if(cooltimeData != null && cooltimeData.timerActivated)
        {
            cooltimeData.DiscountCooltime();
            cooltimeImage.fillAmount = Mathf.Lerp(0, 1, cooltimeData.curTime / cooltimeData.time);
        }
    }

    private void OnDestroy()
    {
        ResourceManager.instance.UnRegisterEvent_WaterChanged(CheckSpawnCost);
        EventManager.UnRegisterEvent<Event_BuildingConstructed>(OnBuildingConstructed);
        EventManager.UnRegisterEvent<Event_BuildingDestroyed>(OnBuildingDestroyed);
    }

    public void InitSlot(UnitData unitData)
    {
        EventManager.RegisterEvent<Event_BuildingConstructed>(OnBuildingConstructed);
        EventManager.RegisterEvent<Event_BuildingDestroyed>(OnBuildingDestroyed);

        this.unitData = unitData;

        // 기본적으로 슬롯은 비활성화 상태
        DisableSlot();
        // 스택은 0으로 시작 및 스택이 0/1일 경우에는 스택 텍스트 비활성화
        stack = 0;
        curStack = stack;
        stackText.gameObject.SetActive(false);

        // 유닛 데이터가 null일 경우 쿨타임 데이터를 null로 초기화
        // 유닛 이미지 오브젝트 비활성화
        // 이후 즉시 반환
        if (unitData == null)
        {
            cooltimeData = null;
            unitImageCP.gameObject.SetActive(false);
            return;
        }

        // 유닛 데이터에 유닛 이미지가 있다면
        // 유닛 이미지 등록 및 유닛 이미지 오브젝트 활성화
        // 아니라면 유닛 이미지 비활성화
        if (unitData.unitIcon != null)
        {
            unitImageCP.gameObject.SetActive(true);
            unitImageCP.sprite = unitData.unitIcon;
        }
        else
        {
            unitImageCP.gameObject.SetActive(false);
        }

        // 쿨타임 데이터 초기화 및 쿨타임 완료 액션 등록
        if(cooltimeData == null)
        {
            cooltimeData = new TimeData(unitData.cooltime);
            cooltimeData.RegisterCooltimeEndAction(OnCooltimeEnd);
        }

        CheckSpawnCost();
    }

    private void OnBuildingConstructed(Event_BuildingConstructed message)
    {
        if (unitData != null && unitData.requireBuilding == message.buildingData)
        {
            AddStack();
        }
    }

    private void OnBuildingDestroyed(Event_BuildingDestroyed message)
    {
        if (unitData != null && unitData.requireBuilding == message.buildingData)
        {
            SubstractStack();
        }
    }

    public void AddStack()
    {
        // 스택이 처음 추가되면 슬롯 활성화
        if (stack == 0)
        {
            EnableSlot();
            CheckSpawnCost();
        }

        // 스택 추가
        stack++;
        // 현재 스택 추가
        curStack++;
        // 현재 스택을 스택 텍스트에 적용
        stackText.text = curStack.ToString();
        // 현재 스택이 2 이상일 경우 스택 텍스트 오브젝트 활성화
        if (stack > 1 && !stackText.gameObject.activeInHierarchy)
        {
            stackText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 슬롯의 스택을 하나 빼는 함수. 
    /// 스택이 1 미만으로 떨어질 경우 슬롯 비활성화
    /// </summary>
    public void SubstractStack()
    {
        // 스택 감소
        stack--;
        // 현재 스택이 감소된 스택보다 클 경우 현재 스택 또한 감소
        if(curStack > stack)
            curStack = stack;
        // 스택 텍스트 적용
        stackText.text = curStack.ToString();
        // 스택이 1 이하로 떨어질 경우 스택 텍스트 오브젝트 비활성화
        if (stack <= 1 && stackText.gameObject.activeInHierarchy)
        {
            stackText.gameObject.SetActive(false);
        }
        // 스택이 1 미만으로 떨어질 경우 이벤트 해지 및 슬롯 비활성화
        if(stack < 1)
        {
            DisableSlot();
        }

    }

    public void EnableSlot()
    {
        locked = false;
        ResourceManager.instance.RegisterEvent_WaterChanged(CheckSpawnCost);
        image_locked.fillAmount = 0;
    }

    public void DisableSlot()
    {
        locked = true;
        ResourceManager.instance.UnRegisterEvent_WaterChanged(CheckSpawnCost);
        image_locked.fillAmount = 1;
    }

    public void OnClicked()
    {
        if (unitData != null)
            Debug.Log(unitData.name);
        else
            Debug.Log("Empty");

        // 슬롯이 비활성화 상태라면 클릭해도 반응 X
        if(locked)
        {
            Debug.Log("Slot Locked");
            return;
        }

        // 스택이 1 이상이고, 자원이 코스트 이상 있다면 유닛 소환 로직 실행
        if (curStack >= 1 && ResourceManager.instance.water >= unitData.cost)
        {
            Debug.Log("Spawn Unit");
            // 자원 감소
            ResourceManager.instance.AddWater(unitData.cost * -1);
            // 현재 스택 감소
            curStack--;
            // 현재 스택 텍스트에 적용
            stackText.text = curStack.ToString();
            Stage curStage = StageManager.instance.curStage;
            //유닛 타입에 따라 이동할 라인 및 소환 위치 바뀜
            switch (unitData.unitType)
            {
                case UnitType.GROUND:
                    SpawnUnit(unitData, curStage.unitSpawnPos_Ground, curStage.curLine);
                    break;
                case UnitType.SKY:
                    SpawnUnit(unitData, curStage.unitSpawnPos_Sky, curStage.line_sky);
                    break;
            }
            
            // 쿨타임 시작
            if(!cooltimeData.timerActivated)
            {
                cooltimeData.StartTimer();
                cooltimeImage.fillAmount = 1;
            }
        }
    }

    /// <summary>
    /// 유닛 소환 함수
    /// </summary>
    /// <param name="unitData">유닛 데이터</param>
    /// <param name="pos">유닛이 소환될 위치 Transform</param>
    /// <param name="line">유닛이 이동할 라인</param>
    private void SpawnUnit(UnitData unitData, Transform pos, StageLine line)
    {
        //유닛 오브젝트 생성
        GameObject newUnitObject = Instantiate(unitData.unitPrefab, pos.position, Quaternion.identity);
        //생성된 유닛의 Unit 컴포넌트 초기화
        Unit newUnitCP = newUnitObject.GetComponent<Unit>();
        newUnitCP.Init(unitData, line.waypoints);
    }

    private void OnCooltimeEnd()
    {
        Debug.Log("Cooltime End");
        curStack++;
        stackText.text = curStack.ToString();
        if (curStack < stack)
        {
            cooltimeData.StartTimer();
            cooltimeImage.fillAmount = 1;
        }
    }

    private void CheckSpawnCost()
    {
        if (locked)
            return;

        image_locked.fillAmount = ResourceManager.instance.water >= unitData.cost ? 0 : 1;
    }
}
