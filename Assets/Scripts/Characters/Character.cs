using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterTargetData {
    public string type;
    public string value;

    public CharacterTargetData (string type, string value)
    {
        this.type = type;
        this.value = value;
    }

    public override string ToString()
    {
        return $"{base.ToString()} {{ type: {type}, value: {value} }}";
    }
}

[Serializable]
public class CharacterOrderData {
    public string type;
    public string character;
    public CharacterTargetData target;

    public CharacterOrderData (string type, string character, CharacterTargetData target)
    {
        this.type = type;
        this.character = character;
        this.target = target;
    }

    public override string ToString()
    {
        return $"{base.ToString()} {{ type: {type}, target: {target} }}";
    }
}

public class Character : MonoBehaviour
{
    public string playerID;
    public string state;
    

    [HideInInspector]
    public CharacterSensor sensor;
    [HideInInspector]
    public CharacterMove move;
    [HideInInspector]
    public CharacterAnimator animator;
    [HideInInspector]
    public CharacterBehaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        sensor = GetComponent<CharacterSensor>();
        move = GetComponent<CharacterMove>();
        animator = GetComponent<CharacterAnimator>();
        behaviour = GetComponent<CharacterBehaviour>();
    }

    public void OnReceiveOrder(CharacterOrderData orderData)
    {
        if(orderData == null) return;
        
        if(orderData.type == "move")
        {
            CheckTarget(orderData.target);
        }
        else
        {
            Stop();
        }
    }

    void CheckTarget (CharacterTargetData targetData)
    {
        if(targetData.type == null || targetData.value == null) return;

        if(targetData.type == "position")
        {
            GoTo(JsonUtility.FromJson<Vector3>(targetData.value));
        }
        else if(targetData.type == "go")
        {
            Debug.Log(targetData.value);
            GoTo(GameObject.Find(targetData.value));
        }
    }

    void GoTo (Vector3 position)
    {
        //Debug.Log(position);
        move.GoTo(position);
    }

    void GoTo (GameObject target)
    {
        //Debug.Log(target);
        move.GoTo(target);
    }

    void Stop ()
    {
        move.agent.isStopped = true;
    }
}
