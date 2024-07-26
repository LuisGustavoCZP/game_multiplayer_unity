using UnityEngine;

public abstract class CompositeNode : BehaviorNode
{
    public BehaviorNode[] children;

    public void SetChildren(BehaviorNode[] children)
    {
        this.children = children;
    }
}