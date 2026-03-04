using UnityEngine;

public class IdleNode : BaseNode
{
    public IdleNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        unit.Idle();

        return INode.state.Success;
    }
}
