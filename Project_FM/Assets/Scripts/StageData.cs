using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;
    public int stageIndex;

    //UnityUnit
    public float lineLength;
}
