using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle Action", menuName = "Actions/Idle")]
public class IdleAction : BehaviourAction
{
    public override bool Check (CharacterBehaviour behaviour)
    {
        return false;
    }

    public override IEnumerator Start(CharacterBehaviour behaviour, ActionData data)
    {
        yield return null; // Retornar Success se a ação for bem-sucedida
    }

    public override IEnumerator Execute(CharacterBehaviour behaviour, ActionData data)
    {
        var character = behaviour.GetComponent<Character>();
        var player = GameObject.Find(character.playerID).GetComponent<Player>();

        //var target = new CharacterTargetData("go", sense.target.name);

        var order = new CharacterOrderData("idle", character.name, null);
        player.SendOrder(order);

        yield return null;
    }

    public override IEnumerator Finish(CharacterBehaviour behaviour, ActionData data)
    {
        yield return null;
    }
}
