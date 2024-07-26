using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGoNextTargetAction", menuName = "Actions/GoNextTarget")]
public class GoNextTargetAction : BehaviourAction
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
        var sensor = character.sensor;
        var player = GameObject.Find(character.playerID).GetComponent<Player>();

        if(sensor.senses.Count == 0) yield return null;

        var sense = sensor.senses[0]; 
        //var target = new CharacterTargetData("go", sense.target.name);
        var target = new CharacterTargetData("position", JsonUtility.ToJson(sense.target.position));
        var order = new CharacterOrderData("move", character.name, target);
        player.SendOrder(order);

        yield return null;
    }

    public override IEnumerator Finish(CharacterBehaviour behaviour, ActionData data)
    {
        yield return null;
    }
}
