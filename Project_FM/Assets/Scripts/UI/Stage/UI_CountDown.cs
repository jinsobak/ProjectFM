using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CountDown : UI
{
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private int countDownSecond;
    WaitForSeconds waitSecond = new WaitForSeconds(1f);

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_StartCountDown>(StartCountDown);
    }

    private void OnDestroy()
    {
        EventManager.UnRegisterEvent<Event_InStage_StartCountDown>(StartCountDown);
    }

    private void StartCountDown(Event_InStage_StartCountDown message)
    {
        countDownSecond = message.countTime;

        EnableUI();
        StartCoroutine(CoCountDown());
    }

    public void EnableUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator CoCountDown()
    {
        if(countDownText == null)
            yield break;

        for(int i = countDownSecond; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return waitSecond;
        }

        countDownText.text = "Start!";
        yield return waitSecond;

        DisableUI();

        EventManager.Publish(new Event_InStage_EndCountDown());
    }
}
