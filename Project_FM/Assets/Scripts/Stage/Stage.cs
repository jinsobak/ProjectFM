using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manage StageData, UnitSpawnLine, UnitSpawnPos
/// </summary>
public class Stage : MonoBehaviour
{
    private StageData stageData = null;

    [Header("Lines")]
    [SerializeField]
    public StageLine line_one;
    [SerializeField]
    public StageLine line_two;
    [SerializeField]
    public StageLine line_sky;

    [Header("Unit SpawnPoints")]
    [SerializeField]
    public Transform unitSpawnPos_Ground;
    [SerializeField]
    public Transform unitSpawnPos_Sky;

    [Header("Player Board")]
    [SerializeField]
    private Board m_board_player;
    public Board Board_Player { get { return m_board_player; } }

    [Header("Enemy Base")]
    private Transform m_enemyBaseTF;
    public Transform EnemyBaseTF { get { return m_enemyBaseTF; } }

    [Header("MapLimit")]
    [SerializeField]
    private Transform m_leftLimitTF;
    public Transform LeftLimitTF { get { return m_leftLimitTF; } }
    [SerializeField]
    private Transform m_rightLimitTF;
    public Transform RightLimitTF { get { return m_rightLimitTF; } }

    // 현재 선택된 라인
    public StageLine curLine { get; private set; }

    private void OnEnable()
    {
        EventManager.RegisterEvent<Event_InStage_ObjectInitalize>(Init);
    }

    private void OnDisable()
    {
        EventManager.UnRegisterEvent<Event_InStage_ObjectInitalize>(Init);
    }


    private void Start()
    {
        EventManager.RegisterEvent<Event_LineChange>(ChangeLine);
    }

    public void Init(Event_InStage_ObjectInitalize initEvent)
    {
        stageData = initEvent.stageData;

        // Set Origin Line (Base: LineOne)
        curLine = line_one != null ? line_one : line_two;

        // Player Board Init
        if(m_board_player != null) 
            m_board_player.Init();

    }

    /// <summary>
    /// 라인 변경 이벤트 발생 시 이벤트의 GroundLinePosition에 따라 라인 변경
    /// </summary>
    /// <param name="lineChangeEvent"></param>
    private void ChangeLine(Event_LineChange lineChangeEvent)
    {
        //이벤트에서 지상 라인 타입을 받아서 저장
        GroundLinePosition linePos = lineChangeEvent.linePos;

        //지상 라인 타입에 따라서 라인 병경
        switch(linePos)
        {
            case GroundLinePosition.UP:
                curLine = line_one;
                Debug.Log("LineChanged. curLine: UP");
                break;
            case GroundLinePosition.DOWN:
                curLine = line_two;
                Debug.Log("LineChanged. curLine: DOWN");
                break;
        }
    }
}
