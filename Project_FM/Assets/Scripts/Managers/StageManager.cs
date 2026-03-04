using System.Diagnostics.Contracts;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum StageState
{
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

    private StageState curState;

    public CameraController cameraController { get; private set; }

    public MockUserData mockUserData;

    // Player Act flags
    public bool canPlayerAct = true;

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

    }

    public void SetCameraController(CameraController controller)
    {
        cameraController = controller;
    }

    private void StageInit(StageData stageData)
    {
        if (curStageData == null || curStage == null)
            return;

        canPlayerAct = false;

        Transform leftLimitTF = curStage.LeftLimitTF;
        Transform rightLimitTF = curStage.RightLimitTF;
        Transform mainbuildingTF_player = curStage.Board_Player.transform;
        Transform mainBuildingTF_enemy = curStage.EnemyBaseTF.transform;

        EventManager.Publish(new Event_InStage_ObjectInitalize
            (
                curStageData,
                leftLimitTF,
                rightLimitTF,
                mainbuildingTF_player,
                mainBuildingTF_enemy
            ));

        ChangeState(StageState.Intro);
    }

    private void ShowIntro()
    {
        cameraController.MoveCamera_Intro();

        ChangeState(StageState.SetUpDeck);
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
                break;
            case StageState.StartCountdown:
                break;
            case StageState.Play:
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
