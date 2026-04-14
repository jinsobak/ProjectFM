using UnityEngine;

public class UI_Panel : UI
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    public void EnableUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
