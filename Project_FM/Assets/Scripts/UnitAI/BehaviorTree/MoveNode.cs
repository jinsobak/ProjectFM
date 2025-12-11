using UnityEngine;

public class MoveNode : BaseNode
{
    public MoveNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        bool isMoving = unit.Move();

        return isMoving ? INode.state.Run : INode.state.Fail;
    }
}
