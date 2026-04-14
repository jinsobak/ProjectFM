using UnityEngine;

public class Panel_Slots_UnitSpawn : UI_Panel
{
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private GameObject spawnSlotPF;

    private Slot_UnitSpawn[] slots;

    public void InitUIWithData(UnitData[] deck)
    {
        // 슬롯 오브젝트 전체 파괴 후 슬롯을 null로 초기화
        if(slots != null)
        {
            foreach (Slot_UnitSpawn slot in slots)
            {
                if(slot != null)
                    Destroy(slot.gameObject);
            }
            slots = null;
        }

        // 덱 길이를 구하고 새 덱 슬롯 배열 생성
        int deckLength = deck.Length;
        slots = new Slot_UnitSpawn[deckLength];

        // 덱 길이 만큼 유닛 슬롯 생성 및 초기화, 슬롯 배열에 슬롯 추가
        for(int i = 0; i < deckLength; i++)
        {
            GameObject slotObject = Instantiate(spawnSlotPF, parent: slotParent);
            Slot_UnitSpawn slotCP = slotObject.GetComponent<Slot_UnitSpawn>();
            slotCP.InitSlot(deck[i]);

            slots[i] = slotCP;
        }

    }


}
