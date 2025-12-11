using UnityEngine;

/// <summary>
/// 유닛 행동 트리를 조립하고 반환하기 위한 베이스 클래스
/// </summary>
public abstract class UnitAIController : MonoBehaviour
{
    //트리의 루트 노드
    protected SelectionNode rootNode;

    /// <summary>
    /// 행동 트리를 조립하기 위한 추상 함수
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public abstract void BuildBT(Unit unit);

    public virtual void EvaluateBT()
    {
        if (rootNode == null)
            return;

        rootNode.Evaluate();
    }
}
