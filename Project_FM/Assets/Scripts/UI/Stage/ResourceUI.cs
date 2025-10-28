using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    private void Start()
    {
        if(ResourceManager.instance != null)
            ResourceManager.instance.OnWaterAmountChanged += ReWriteText;
    }

    private void ReWriteText()
    {
        text.text = ResourceManager.instance.water.ToString();
    }
}
