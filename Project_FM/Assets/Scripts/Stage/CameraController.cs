using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("IntroData")]
    [SerializeField]
    private float reachTime;
    [SerializeField]
    private float waitTime;
    private Vector3 originPos;

    [Header("BaseData")]
    private Camera mainCamera;
    [SerializeField]
    private float moveSpeed;
    private Transform leftLimit;
    private Transform rightLimit;
    private Transform playerBaseTF;
    private Transform enemyBaseTF;

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_CameraInitStart>(Init);
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_InStage_CameraInitStart>(Init);
    }

    private void Init(Event_InStage_CameraInitStart message)
    {
        mainCamera = Camera.main;

        leftLimit = message.leftLimitTF;
        rightLimit = message.rightLimitTF;
        playerBaseTF = message.mainbuildingTF_player;
        enemyBaseTF = message.mainBuildingTF_enemy;

        mainCamera.transform.position = new Vector3(0, 0, -10);

        StageManager.instance.SetCameraController(this);

        EventManager.Publish(new Event_InStage_CameraInitEnd());
    }

    public void MoveCamera_Intro(Action onComplete)
    {
        originPos = mainCamera.transform.position;
        StartCoroutine(CoMoveCamera_Intro(onComplete));
    }

    private IEnumerator CoMoveCamera_Intro(Action onComplete)
    {
        float elapsedTime = 0f;
        float newCameraX = 0f;
        while (elapsedTime < reachTime)
        {
            elapsedTime += Time.deltaTime;
            newCameraX = Mathf.Lerp(originPos.x, enemyBaseTF.position.x, elapsedTime / reachTime);
            newCameraX = Mathf.Min(newCameraX, rightLimit.position.x);
            mainCamera.transform.position = new Vector3(newCameraX, 0, -10);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(waitTime);

        elapsedTime = 0f;
        Vector2 curCameraPos = mainCamera.transform.position;
        while (elapsedTime < reachTime)
        {
            elapsedTime += Time.deltaTime;
            newCameraX = Mathf.Lerp(curCameraPos.x, originPos.x, elapsedTime / reachTime);
            newCameraX = Math.Max(newCameraX, leftLimit.position.x);
            mainCamera.transform.position = new Vector3(newCameraX, 0, -10);

            yield return null;
        }

        onComplete.Invoke();
    }
}
