using UnityEngine;

[CreateAssetMenu(fileName = "NewSequenceNode", menuName = "BehaviorTree/SequenceNode")]
public class SequenceNode : CompositeNode
{
    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        foreach (var child in children)
        {
            if (child.Execute(behaviour) == NodeState.Failure)
            {
                return NodeState.Failure; // Se algum filho falhar, a sequência falha
            }
        }
        return NodeState.Success; // Todos os filhos foram bem-sucedidos
    }
}