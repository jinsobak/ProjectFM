using UnityEngine;

public class Baechudoll : Unit
{
    public override void Init(UnitData _data, Transform[] waypointsArr)
    {
        base.Init(_data, waypointsArr);
        Debug.Log("배추돌이 소환");
    }
}
