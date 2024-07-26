using UnityEngine;

[CreateAssetMenu(fileName = "NewActionNode", menuName = "BehaviorTree/ActionNode")]
public class ActionNode : BehaviorNode
{
    public BehaviourAction action;
    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        return action.Execute(behaviour); // Retornar Success se a ação for bem-sucedida
    }
}