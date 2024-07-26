using UnityEngine;

[CreateAssetMenu(fileName = "NewSelectorNode", menuName = "BehaviorTree/SelectorNode")]
public class SelectorNode : CompositeNode
{
    public override NodeState Execute(CharacterBehaviour behaviour)
    {
        foreach (var child in children)
        {
            if (child.Execute(behaviour) == NodeState.Success)
            {
                return NodeState.Success; // Se algum filho for bem-sucedido, a seleção é bem-sucedida
            }
        }
        return NodeState.Failure; // Todos os filhos falharam
    }
}