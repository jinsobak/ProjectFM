using System.Collections.Generic;
using UnityEngine;

public class BuildingArea : MonoBehaviour
{
    private int buildingSlotCount = 5;
    [SerializeField]
    private GameObject buildingSlotPrefab;
    [SerializeField]
    private Transform[] buildingSlotPoses;

    private List<Slot_building> list_buildingSlot = new List<Slot_building>();

    /// <summary>
    /// 건물을 건설하기 위한 슬롯을 생성합니다.
    /// </summary>
    /// <param name="buildingSlotCount">생성할 슬롯의 개수</param>
    public void Init_buildingArea(int  buildingSlotCount)
    {
        BuildManager.instance.SetArea(this);

        this.buildingSlotCount = buildingSlotCount;

        //전달받은 생성할 슬롯 개수가 슬롯 위치 배열의 길이보다 적다면 슬롯 개수 사용, 아니라면 배열의 길이 사용
        int slotCreateCount = buildingSlotCount <= buildingSlotPoses.Length ? buildingSlotCount : buildingSlotPoses.Length;

        for (int i = 0; i < buildingSlotPoses.Length; i++)
        {
            // 슬롯 생성및 위치 초기화
            GameObject newSlotObj = Instantiate(buildingSlotPrefab, buildingSlotPoses[i]);
            newSlotObj.transform.localPosition = Vector3.zero;

            // 생성할 슬롯 리스트에 저장 및 초기화 함수 실행
            Slot_building newSlotCP = newSlotObj.GetComponent<Slot_building>();
            newSlotCP.Init(CellType.Empty, i);
            list_buildingSlot.Add(newSlotCP);
        }
    }

    /// <summary>
    /// 건물을 건설하는 함수
    /// </summary>
    /// <param name="buildingPF">건설할 건물의 프리팹</param>
    /// <param name="slotIndex">건설할 슬롯의 인덱스</param>
    public void BuildBuilding(GameObject buildingPF, int slotIndex)
    {
        //슬롯에 건물을 건설할 수 있는지 확인
        if (!CanBuild(slotIndex))
            return;

        Debug.Log("Build Building");

        // 건물 오브젝트 생성 및 초기화
        GameObject newBuilding = Instantiate(buildingPF, list_buildingSlot[slotIndex].transform);
        newBuilding.transform.localPosition = Vector3.zero;
        Building newBuildingCP = newBuilding.GetComponent<Building>();
        newBuildingCP.SetIndex(slotIndex);
        // 건물이 지어질 때 실행하는 함수 실행
        newBuildingCP.OnConstruct();

        // 건물이 지어진 슬롯의 타입을 Constructed로 변경
        list_buildingSlot[slotIndex].SetType(CellType.Constructed);
    }

    /// <summary>
    /// 슬롯에 건물이 건설 가능한지 확인하는 함수
    /// </summary>
    /// <param name="slotIndex">건물을 건설할 슬롯의 인덱스</param>
    /// <returns></returns>
    private bool CanBuild(int slotIndex)
    {
        //슬롯에 이미 건물이 건설되어 있다면 false 반환
        if (list_buildingSlot[slotIndex].cellType == CellType.Constructed)
            return false;

        return true;
    }

    /// <summary>
    /// 건물을 파괴하는 함수
    /// </summary>
    /// <param name="building">파괴할 건물의 오브젝트</param>
    public void DestroyBuilding(GameObject building)
    {
        Debug.Log("Destroy Building");

        // 건물의 Building 컴포넌트를 가져와 컴포넌트의 slotIndex를 참조
        Building buildingCP = building.GetComponent<Building>();
        int index = buildingCP.slotIndex;

        list_buildingSlot[index].SetType(CellType.Empty);

        EventManager.Publish(new Event_BuildingDestroyed(buildingCP.buildingData.producableUnitList));
        Destroy(building);
    }
}
