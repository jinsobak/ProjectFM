using UnityEngine;

public class FindTargetNode : BaseNode
{
    public FindTargetNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        bool targetFound = unit.FindTarget();

        if(targetFound && unit.IsTargetInRange())
        {
            return INode.state.Success;
        }
        else
        {
            return INode.state.Fail;
        }
    }
}
