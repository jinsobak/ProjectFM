using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image unitImageCP;
    [SerializeField]
    private TextMeshProUGUI keyText;
    [SerializeField]
    private TextMeshProUGUI stackText;

    private UnitData unitData;
    private int stack;
    private string key;

    public void InitSlot(UnitData unitData)
    {
        this.unitData = unitData;

    }

    public void AddStack()
    {
        stack++;
        stackText.text = stack.ToString();
    }
}
