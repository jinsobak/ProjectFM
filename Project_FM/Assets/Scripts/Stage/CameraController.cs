using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("IntroData")]
    [SerializeField]
    private float enemyBaseReachTime;
    private Vector3 originPos;

    [Header("BaseData")]
    [SerializeField]
    private float moveSpeed;
    private Transform leftLimit;
    private Transform rightLimit;
    private Transform playerBaseTF;
    private Transform enemyBaseTF;

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_ObjectInitalize>(Init);
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_InStage_ObjectInitalize>(Init);
    }

    private void Init(Event_InStage_ObjectInitalize initEvent)
    {
        leftLimit = initEvent.leftLimitTF;
        rightLimit = initEvent.rightLimitTF;
        playerBaseTF = initEvent.mainbuildingTF_player;
        enemyBaseTF = initEvent.mainBuildingTF_enemy;

        Camera.main.transform.position = Vector3.zero;

        StageManager.instance.SetCameraController(this);
    }

    public void MoveCamera_Intro()
    {
        originPos = transform.position;
    }

    private IEnumerator CoMoveCamera_Intro()
    {

        yield return null;
    }
}
