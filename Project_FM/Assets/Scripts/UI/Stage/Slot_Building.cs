using UnityEngine;
using UnityEngine.UI;

public class Slot_Building : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingPF;
    private Sprite image;
    private Image imageCP;

    public void InitUI()
    {

    }    

    public void OnClick()
    {
        if(buildingPF == null)
        {
            Debug.Log("Slot is Empty");
            return;
        }

        Debug.Log("Building Selected");
        BuildManager.instance.ChangeBuildMode(buildMode.Construct);
        BuildManager.instance.SelectBuilding(buildingPF);
    }
}
