using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("Basic Stage Data")]
    public string stageName;
    public int stageId;

    [Header("WaveData")]
    public List<EnemyWaveData> enemyWaveData;
}
