using UnityEngine;

public class AttackNode : BaseNode
{
    public AttackNode(Unit unit) : base(unit) { }

    public override INode.state Evaluate()
    {
        unit.StartAttack();

        return INode.state.Success;
    }
}
