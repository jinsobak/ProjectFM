using System.Diagnostics.Contracts;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public enum StageState
{
    None,
    Initalize,
    Intro,
    SetUpDeck,
    StartCountdown,
    Play,
    Ended,
    Result,
    Pause
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public Stage curStage { get; private set; } = null;
    public StageData curStageData;

    private StageState curState = StageState.None;

    public CameraController cameraController { get; private set; }

    public MockUserData mockUserData;

    // Player Act flags
    public bool canPlayerAct = true;
    [SerializeField]
    private int countTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);

            RegisterEvents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(StageState.Initalize);
    }

    private void RegisterEvents()
    {
        EventManager.RegisterEvent<Event_InStage_StageInitEnd>(StageInitEnd);
        EventManager.RegisterEvent<Event_InStage_CameraInitEnd>(CameraInitEnd);
        EventManager.RegisterEvent<Event_InStage_EndDeckSetUp>(DeckSetUpEnd);
        EventManager.RegisterEvent<Event_InStage_EndCountDown>(CountDownEnd);
    }

    public void SetCurStage(Stage stage)
    {
        curStage = stage;
    }

    public void SetCameraController(CameraController controller)
    {
        cameraController = controller;
    }

    private void StageInit(StageData stageData)
    {
        if (curStageData == null)
            return;

        canPlayerAct = false;

        EventManager.Publish(new Event_InStage_StageInitStart(stageData));
    }

    private void StageInitEnd(Event_InStage_StageInitEnd message)
    {
        Debug.Log("Stage Init End");
        CameraInit();
    }

    private void CameraInit()
    {
        Debug.Log("Start CameraInit");
        Debug.Log(curStage == null);

        Transform leftLimitTF = curStage.LeftLimitTF;
        Transform rightLimitTF = curStage.RightLimitTF;
        Transform mainbuildingTF_player = curStage.Board_Player.MainBuilding.transform;
        Transform mainBuildingTF_enemy = curStage.EnemyBaseTF.transform;

        EventManager.Publish(new Event_InStage_CameraInitStart
            (
                leftLimitTF,
                rightLimitTF,
                mainbuildingTF_player,
                mainBuildingTF_enemy
            ));
    }

    private void CameraInitEnd(Event_InStage_CameraInitEnd message)
    {
        Debug.Log("Camera Init End");
        ChangeState(StageState.Intro);
    }

    private void ShowIntro()
    {
        Debug.Log("Start Stage Intro");
        cameraController.MoveCamera_Intro(
                () => {
                    Debug.Log("Stage Intro End");
                    ChangeState(StageState.SetUpDeck);
                }
            );
    }
 
    private void StartDeckSetUp()
    {
        Debug.Log("Start Deck SetUp");
        EventManager.Publish(new Event_InStage_StartDeckSetUp());
    }

    private void DeckSetUpEnd(Event_InStage_EndDeckSetUp message)
    {
        Debug.Log("End Deck SetUp");

        ChangeState(StageState.StartCountdown);
    }

    private void CountDownStart()
    {
        Debug.Log("Start CoundDown");
        // 플레이어 조작 비활성화
        canPlayerAct = false;

        // 카운트 다운 시작 이벤트 발행
        EventManager.Publish(new Event_InStage_StartCountDown(countTime));
    }

    private void CountDownEnd(Event_InStage_EndCountDown massage)
    {
        Debug.Log("End CountDown");

        ChangeState(StageState.Play);
    }

    private void PlayStart()
    {

    }

    /// <summary>
    /// Changes StageStage and StateChangeMethod
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(StageState newState)
    {
        // if newStageState is same with curStageState, nothing happen
        if (newState == curState)
            return;

        curState = newState;

        switch (curState)
        {
            case StageState.Initalize:
                StageInit(curStageData);
                break;
            case StageState.Intro:
                ShowIntro();
                break;
            case StageState.SetUpDeck:
                StartDeckSetUp();
                break;
            case StageState.StartCountdown:
                CountDownStart();
                break;
            case StageState.Play:
                PlayStart();
                break;
            case StageState.Ended:
                break;
            case StageState.Result:
                break;
            case StageState.Pause:
                break;
        }
    }
}
