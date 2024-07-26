using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitData
{
    public string id;
    public string player_id;
    public int job_id;
    public int skin_id;
    public string name;
    public object stats;
    public object skills;
    public object behaviours;
    public object equipments;
    public string created_at;
    public string updated_at;
    public string deleted_at;

    public override string ToString()
    {
        return base.ToString();
    }
}

public class GameManager : MonoBehaviour
{
    bool isGameStarted = false;
    [Space(20)]
    public UnityEvent OnStart;
    public UnityEvent<string> OnMatchStart;
    public UnityEvent<WSMessage> OnSendMessage;

    public Player player = null;
    public GameObject[] characterList;
    readonly Dictionary<string, GameObject> characters = new();

    void Start()
    {
        player = GetComponentInChildren<Player>();
        characters.Clear();
        foreach (var character in characterList)
        {
            characters.Add(character.name, character);
        }
        OnStart.Invoke();
    }
    
    void Awake()
    {
        
    }

    void Update()
    {
        if(!isGameStarted) return;
    }

    public void OnGameStart()
    {
        isGameStarted = true;
    }

    public void OnGameEnd()
    {
        isGameStarted = false;
    }

    public void HandleStartMatch ()
    {
        OnMatchStart.Invoke(player.token);
    }

    public void OnGameMessage(WSMessage message)
    {
        //Debug.Log(message);
        if(message.type == "order")
        {
            var orderData = JsonUtility.FromJson<CharacterOrderData>(message.data);
            //Debug.Log(orderData);

            var character = characters[orderData.character];
            
            //Debug.Log($"{character.name} Received OnUpdate!", character);
            character.SendMessage("OnReceiveOrder", orderData);
        }
    }

    public void SendOrderMessage(CharacterOrderData orderData)
    {
        var orderJson = JsonUtility.ToJson(orderData);
        var message = new WSMessage("order", orderJson);
        OnSendMessage.Invoke(message);
    }
}
