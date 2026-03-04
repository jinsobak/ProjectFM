using UnityEngine;

public class CheckAttackingNode : BaseNode
{
    public CheckAttackingNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        if (unit.isAttacking)
        {
            return INode.state.Run;
        }
        else
        {
            return INode.state.Fail;
        }
    }
}
