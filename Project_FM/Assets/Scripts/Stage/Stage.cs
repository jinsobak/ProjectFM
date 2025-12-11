using UnityEngine;
using UnityEngine.InputSystem;

public class Stage : MonoBehaviour
{
    private StageData stageData = null;

    [SerializeField]
    private BuildingGrid grid;

    [SerializeField]
    public StageLine line_one;
    [SerializeField]
    public StageLine line_two;
    [SerializeField]
    public StageLine line_sky;

    [SerializeField]
    public Transform unitSpawnPos_Ground;
    [SerializeField]
    public Transform unitSpawnPos_Sky;

    public StageLine curLine { get; private set; }

    public int resource { get; private set; } = 0;

    private void Start()
    {
        StageManager.instance.SetStage(this);
        EventManager.RegisterEvent<Event_LineChange>(ChangeLine);
    }

    public void InitStage(StageData stageData)
    {
        this.stageData = stageData;
        BuildStage();
    }

    private void BuildStage()
    {
        grid.InitGrid(5, 5);
        BuildManager.instance.SetGrid(grid);
        if(line_one != null)
        {
            curLine = line_one;
        }
        else
        {
            curLine = line_two;
        }
    }

    public BuildingGrid GetGrid()
    {
        return grid;
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
