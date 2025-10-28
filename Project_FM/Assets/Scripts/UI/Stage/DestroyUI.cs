using UnityEngine;

public class DestroyUI : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedBox;

    private bool activated = false;

    private void Start()
    {
        BuildManager.instance.RegisterDestroyModeEnd(DisableUI);
    }

    public void OnClick()
    {
        if(!activated)
        {
            Debug.Log("Destroy Activate");
            BuildManager.instance.ChangeBuildMode(buildMode.Destroy);
            activated = true;
            selectedBox.SetActive(true);
        }
    }

    private void DisableUI()
    {
        Debug.Log("DestroyUI Disable");
        activated = false;
        selectedBox.SetActive(false);
    }
}
