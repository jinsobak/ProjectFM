using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface INode
{
    public enum state { Run, Success, Fail };

    public INode.state Evaluate();
}

public abstract class BaseNode : INode
{
    protected Unit unit;

    public BaseNode(Unit unit)
    {
        this.unit = unit;
    }

    public abstract INode.state Evaluate();
}
public class ActionNode : INode
{
    public Func<INode.state> action;

    public ActionNode(Func<INode.state> action)
    {
        this.action = action;
    }

    public INode.state Evaluate()
    {
        if(action != null)
        {
            return action.Invoke();
        }
        else
        {
            return INode.state.Fail;
        }
    }
}

public class SelectionNode : INode
{
    List<INode> children;

    public SelectionNode()
    {
        children = new List<INode>();
    }

    public void Add(INode node)
    {
        children.Add(node);
    }

    public INode.state Evaluate()
    {
        if (children.Count <= 0)
            return INode.state.Fail;

        foreach(INode child in children)
        {
            switch(child.Evaluate())
            {
                case INode.state.Success:
                    return INode.state.Success;
                case INode.state.Run:
                    return INode.state.Run;
            }
        }
        return INode.state.Fail;
    }
}

public class SequenceNode : INode
{
    List<INode> children;

    public SequenceNode()
    {
        children = new List<INode>();
    }

    public void Add(INode node)
    {
        children.Add(node);
    }

    public INode.state Evaluate()
    {
        if(children.Count <= 0)   
            return INode.state.Fail;

        foreach(INode child in children) 
        {
            switch(child.Evaluate())
            {
                case INode.state.Run:
                    return INode.state.Run;
                case INode.state.Success:
                    continue;
                case INode.state.Fail:
                    return INode.state.Fail;
            }
            
        }
        return INode.state.Success;
    }
}

public class RandomSelectionNode : INode
{
    List<INode> children;

    public RandomSelectionNode()
    {
        children = new List<INode>();
    }

    public void Add(INode node)
    {
        children.Add(node);
    }

    public INode.state Evaluate()
    {
        if (children.Count <= 0)
            return INode.state.Fail;

        int randomIndex = UnityEngine.Random.Range(0, children.Count); // 무작위 선택
        INode.state result = children[randomIndex].Evaluate();

        return result; // 선택된 노드의 결과 반환
    }
}

public class ConditionNode : INode
{
    public Func<bool> action;
    public ActionNode child;

    public ConditionNode(Func<bool> action)
    {
        this.action = action;
    }

    public void Add(ActionNode node)
    {
        child = node;
    }

    public INode.state Evaluate()
    {
        if (child == null)
            return INode.state.Fail;
        
        if(action.Invoke() == true)
        {
            return child.Evaluate();
        }
        else
        {
            return INode.state.Fail;
        }
    }
}