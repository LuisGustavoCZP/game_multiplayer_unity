using UnityEngine;

[CreateAssetMenu(fileName = "NewRepeatNode", menuName = "BehaviorTree/RepeatNode")]
public class RepeatNode : BehaviorNode
{
    public BehaviorNode child;

    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        while (true)
        {
            var result = child.Execute(behaviour);
            if (result == NodeState.Failure)
            {
                return NodeState.Failure;
            }
        }
    }
}