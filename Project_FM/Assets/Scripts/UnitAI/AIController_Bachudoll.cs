using UnityEngine;

public class AIController_Bachudoll : UnitAIController
{
    public override void BuildBT(Unit unit)
    {
        Debug.Log("Build BT");

        rootNode = new SelectionNode();

        MoveNode moveNode = new MoveNode(unit);
        rootNode.Add(moveNode);
    }
}
