using UnityEngine;

public enum UnitType
{
    GROUND,
    SKY
}

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite unitIcon;
    public GameObject unitPrefab;
    public int cost;
    public int cooltime;
    public UnitType unitType;

    [Header("Status")]
    //최대 체력
    public int baseMaxHp;
    //이동 속도
    public float baseMoveSpeed;
    //공격 사거리
    public float baseAttackRange;
    //공격 쿨타임
    public float baseAttackRate;
    //공격력
    public float baseAttackDamage;

    [Header("AttackAnimation")]
    public float preDelay;
    public float postDelay;
}
