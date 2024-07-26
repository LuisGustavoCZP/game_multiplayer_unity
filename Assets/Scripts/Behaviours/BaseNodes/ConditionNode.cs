using UnityEngine;

[CreateAssetMenu(fileName = "NewSensorCondition", menuName = "BehaviorTree/ConditionNodes/SensorCondition")]
public class ConditionNode : BehaviorNode
{
    public Condition[] conditions = new Condition[0];
    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        foreach (var condition in conditions)
        {
            if(!condition.Check(behaviour)) return NodeState.Failure;
        }
        return NodeState.Success;
    }
}