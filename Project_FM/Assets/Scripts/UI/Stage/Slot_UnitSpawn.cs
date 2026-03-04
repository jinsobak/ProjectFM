using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UnitSpawn : MonoBehaviour
{
    [Header("SlotData")]
    [SerializeField]
    private Image cooltimeImage;
    [SerializeField]
    private Image unitImageCP;
    [SerializeField]
    private TextMeshProUGUI keyText;
    [SerializeField]
    private TextMeshProUGUI stackText;

    public UnitData unitData { get; private set; }
    private int stack;
    private string key;

    [Header("UnitSpawnData")]
    private int curStack;
    private TimeData cooltimeData;

    //슬롯 파괴시 실행할 슬롯 파괴 액션
    public event Action<Slot_UnitSpawn> action_slotDestroy;

    private void Update()
    {
        if(cooltimeData.timerActivated)
        {
            cooltimeData.DiscountCooltime();
            cooltimeImage.fillAmount = Mathf.Lerp(0, 1, cooltimeData.curTime / cooltimeData.time);
        }
    }

    public void InitSlot(UnitData unitData)
    {
        this.unitData = unitData;
        if(unitData.unitIcon != null)
        {
            unitImageCP.sprite = unitData.unitIcon;
        }
        if(stack <= 1)
        {
            stack = 1;
            curStack = 1;
            stackText.gameObject.SetActive(false);
        }
        cooltimeData = new TimeData(unitData.cooltime);
        cooltimeData.RegisterCooltimeEndAction(OnCooltimeEnd);
        CheckSpawnCost();
        ResourceManager.instance.RegisterEvent_WaterChanged(CheckSpawnCost);
    }

    public void AddStack()
    {
        stack++;
        curStack++;
        stackText.text = curStack.ToString();
        if (stack > 1 && !stackText.gameObject.activeInHierarchy)
        {
            stackText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 슬롯의 스택을 하나 빼는 함수 스택이 1 미만일 경우 슬롯을 파괴한다.
    /// </summary>
    public void SubstractStack()
    {
        stack--;
        if(curStack > stack)
            curStack = stack;
        stackText.text = curStack.ToString();
        if (stack <= 1 && stackText.gameObject.activeInHierarchy)
        {
            stackText.gameObject.SetActive(false);
        }

        if(stack < 1)
        {
            ResourceManager.instance.UnRegisterEvent_WaterChanged(CheckSpawnCost);
            action_slotDestroy?.Invoke(this);
            Destroy(gameObject);
        }

    }

    public void OnClicked()
    {
        if (curStack >= 1 && ResourceManager.instance.water >= unitData.cost)
        {
            Debug.Log("Spawn Unit");
            ResourceManager.instance.AddWater(unitData.cost * -1);
            curStack--;
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
        cooltimeImage.fillAmount = ResourceManager.instance.water >= unitData.cost ? 0 : 1;
    }
}
