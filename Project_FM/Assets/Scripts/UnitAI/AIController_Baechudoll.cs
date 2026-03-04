using UnityEngine;

public class AIController_Baechudoll : UnitAIController
{
    public override void BuildBT(Unit unit)
    {
        Debug.Log("Build BT");
        // root
        rootNode = new SelectionNode();
        // 1-1 CheckAttacking
        rootNode.Add(new CheckAttackingNode(unit));
        // 1-2 Attack tree (Sequence)
        SequenceNode attackSequence = new SequenceNode();
        rootNode.Add(attackSequence);
        // 2-1 Find Target
        // if target exist in attackRange: return success --> execute tryAttackTree
        // else: return fail --> execute move
        FindTargetNode findTargetNode = new FindTargetNode(unit);
        attackSequence.Add(findTargetNode);
        // 2-2 TryAttack Tree (Selection)
        SelectionNode tryAttackSelection = new SelectionNode();
        attackSequence.Add(tryAttackSelection);
        // 3-1 TryAttack Tree2 (Sequence)
        SequenceNode tryAttackSequence = new SequenceNode();
        tryAttackSelection.Add(tryAttackSequence);
        // 4-1 Check Attack Cooltime
        // if is cooltime : return Fail --> execute idle
        // else return success --> execute attack
        CheckAttackCooltimeNode checkAtkCooltimeNode = new CheckAttackCooltimeNode(unit);
        // 4-2 Attack
        AttackNode attackNode = new AttackNode(unit);
        tryAttackSequence.Add(checkAtkCooltimeNode);
        tryAttackSequence.Add(attackNode);
        // 3-2 Idle during attack cooltime
        IdleNode idleNode = new IdleNode(unit);
        tryAttackSelection.Add(idleNode);

        // 1-3 Move
        MoveNode moveNode = new MoveNode(unit);
        rootNode.Add(moveNode);

        // 1-4 Idle
        rootNode.Add(idleNode);
    }
}
