using UnityEngine;

public class Slot_building : MonoBehaviour, IInteractable
{
    public CellType cellType { get; private set; } = CellType.None;

    public int slotIndex { get; private set; } = -1;

    public void Init(CellType type, int index)
    {
        SetType(type);
        SetIndex(index);
    }

    public void SetIndex(int index)
    {
        this.slotIndex = index;
    }

    public void SetType(CellType type)
    {
        cellType = type;
    }

    public void Interact()
    {
        Debug.Log("Slot Interact");
        BuildManager.instance.OnCellClicked(slotIndex);
    }
}
