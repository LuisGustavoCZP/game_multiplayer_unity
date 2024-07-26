using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class HTTPResponse<T> where T : class
{
    public T data;

    /* public T Parse ()
    {
        return JsonUtility.FromJson<T>(data);
    } */

    public override string ToString()
    {
        return base.ToString();
    }
}

public class HTTPManager
{
    // URL base do seu servidor
    private readonly string baseURL = "https://api.example.com/";
    public HTTPManager (string baseUrl)
    {
        baseURL = baseUrl;
        
    }

    // Método para realizar um POST request
    public IEnumerator PostRequest(string endpoint, WWWForm formData, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Post(baseURL + endpoint, formData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"POST Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    public IEnumerator PostRequest(string endpoint, string authToken, WWWForm formData, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Post(baseURL + endpoint, formData);
        www.SetRequestHeader("Authorization", authToken);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"POST Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    // Método para realizar um GET request
    public IEnumerator GetRequest(string endpoint, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Get(baseURL + endpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"GET Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    public IEnumerator GetRequest(string endpoint, string authToken, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Get(baseURL + endpoint);
        www.SetRequestHeader("Authorization", authToken);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"GET Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    // Método para realizar um PUT request
    public IEnumerator PutRequest(string endpoint, string jsonData, System.Action<string> callback)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using UnityWebRequest www = UnityWebRequest.Put(baseURL + endpoint, bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"PUT Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    public IEnumerator PutRequest(string endpoint, string authToken, string jsonData, System.Action<string> callback)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using UnityWebRequest www = UnityWebRequest.Put(baseURL + endpoint, bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", authToken);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"PUT Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    // Método para realizar um DELETE request
    public IEnumerator DeleteRequest(string endpoint, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Delete(baseURL + endpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"DELETE Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            //Debug.Log($"DELETE Success: {www.downloadHandler.text}");
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    public IEnumerator DeleteRequest(string endpoint, string authToken, System.Action<string> callback)
    {
        using UnityWebRequest www = UnityWebRequest.Delete(baseURL + endpoint);
        www.SetRequestHeader("Authorization", authToken);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"DELETE Error: {www.error}");
            callback?.Invoke(null);
        }
        else
        {
            //Debug.Log($"DELETE Success: {www.downloadHandler.text}");
            callback?.Invoke(www.downloadHandler.text);
        }
    }
}