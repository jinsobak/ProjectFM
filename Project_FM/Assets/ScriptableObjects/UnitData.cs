using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Sprite unitIcon;
    public GameObject unitPrefab;
    public int cost;

    [Header("Status")]
    public int baseMaxHp;
}
