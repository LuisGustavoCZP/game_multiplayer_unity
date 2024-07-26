using UnityEngine;
using UnityEngine.Events;
using NativeWebSocket;

[System.Serializable]
public class WSMessage
{
  public string type;
  public string data;

  public WSMessage (string type, string data)
  {
    this.type = type;
    this.data = data;
  }

  public override string ToString()
  {
    return base.ToString();
  }
}

public class NetworkManager : MonoBehaviour
{
  WebSocket websocket;
  public string host = "ws://localhost:3000";
  public UnityEvent OnOpen;
  public UnityEvent OnError;
  public UnityEvent OnClose;
  public UnityEvent<WSMessage> OnMessage;

  public bool started = false;

  // Start is called before the first frame update
  void Start()
  {
    
  }

  public void StartClient(string token)
  {
    websocket = new WebSocket($"{host}");

    websocket.OnOpen += () =>
    {
      started = true;
      Debug.Log("Connection opened!");
      OnOpen.Invoke();
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
      OnError.Invoke();
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
      OnClose.Invoke();
    };

    websocket.OnMessage += HandleReceiveMessage;
    
    websocket.Connect();
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      if(started) websocket.DispatchMessageQueue();
    #endif
  }

  public void HandleReceiveMessage(byte[] bytes)
  {
      var message = System.Text.Encoding.UTF8.GetString(bytes);
      //Debug.Log($"Receive {message}");
      var parsedMessage = JsonUtility.FromJson<WSMessage>(message);
      OnMessage.Invoke(parsedMessage);
  }

  public async void SendNetworkMessage(WSMessage message)
  {
    var messageJson = JsonUtility.ToJson(message);

    if (websocket.State == WebSocketState.Open)
    {
      Debug.Log($"Sending {messageJson}");
      await websocket.SendText(messageJson);
      // Sending bytes
      //await websocket.Send(message);

      // Sending plain text
      //await websocket.SendText("plain text message");
    }
  }

  private async void OnApplicationQuit()
  {
    if(started) await websocket.Close();
  }
}