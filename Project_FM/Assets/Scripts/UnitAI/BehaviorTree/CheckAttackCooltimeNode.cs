using UnityEngine;

public class CheckAttackCooltimeNode : BaseNode
{
    public CheckAttackCooltimeNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        return unit.IsAttackCooltime() ? INode.state.Success : INode.state.Fail;
    }
}
