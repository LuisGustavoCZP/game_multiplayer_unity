using UnityEngine;

[CreateAssetMenu(fileName = "NewParallelNode", menuName = "BehaviorTree/ParallelNode")]
public class ParallelNode : CompositeNode
{
    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        bool anyChildRunning = false;

        foreach (var child in children)
        {
            var result = child.Execute(behaviour);
            if (result == NodeState.Failure)
            {
                return NodeState.Failure;
            }
            if (result == NodeState.Running)
            {
                anyChildRunning = true;
            }
        }
        return anyChildRunning ? NodeState.Running : NodeState.Success;
    }
}