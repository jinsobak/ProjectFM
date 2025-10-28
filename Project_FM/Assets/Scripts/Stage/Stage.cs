using UnityEngine;
using UnityEngine.InputSystem;

public class Stage : MonoBehaviour
{
    private StageData stageData = null;

    [SerializeField]
    private BuildingGrid grid;

    private GameObject line_one;
    private GameObject line_two;
    private GameObject line_sky;

    public int resource { get; private set; } = 0;

    private void Start()
    {
        StageManager.instance.SetStage(this);
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
    }

    public BuildingGrid GetGrid()
    {
        return grid;
    }
}
