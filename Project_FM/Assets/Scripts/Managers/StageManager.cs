using System.Diagnostics.Contracts;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public StageData stageData;

    public Stage curStage { get; private set; } = null;
    public int curStageIndex = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetStage(Stage _stage)
    {
        curStage = _stage;
        if (curStage != null)
        {
            StartStage();
        }
    }

    private void StartStage()
    {
        curStage.InitStage(stageData);
        curStageIndex = stageData.stageIndex;
    }
 
}
