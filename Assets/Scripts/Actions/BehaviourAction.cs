using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourAction : ScriptableObject
{
    public abstract bool Check (CharacterBehaviour behaviour);

    public abstract IEnumerator Start(CharacterBehaviour behaviour, ActionData data);

    public abstract IEnumerator Execute(CharacterBehaviour behaviour, ActionData data);

    public abstract IEnumerator Finish(CharacterBehaviour behaviour, ActionData data);
}
