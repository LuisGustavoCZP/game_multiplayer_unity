using UnityEngine;

public abstract class BehaviorNode : ScriptableObject
{
    public abstract NodeState Execute(CharacterBehaviour behaviour);
}