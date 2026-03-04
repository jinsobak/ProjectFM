using UnityEngine;

[System.Serializable]
public class EnemyWaveData
{
    [Header("SpawnData")]
    public int spawnTime;
    public UnitData spawnUnit;
    public int spawnCount;
    public float spawnInterval = 0.02f;

    [Header("SpawnLineData")]
    public GroundLinePosition groundLinePos;
}
