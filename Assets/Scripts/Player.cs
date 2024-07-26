using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Token
{
    public string token;

    public override string ToString()
    {
        return base.ToString();
    }
}

public class Player : MonoBehaviour
{
    public string hostname = "http://localhost:3000/api";
    HTTPManager http;
    public string token = "";
    public UnitData[] heroes;
    [Space]
    public UnityEvent<string> OnAuthenticated;
    [Space]
    public UnityEvent OnUnauthenticated;
    [Space]
    public UnityEvent<CharacterOrderData> OnSendOrder;
    private Camera mainCamera;   // Reference to the main camera

    void Start ()
    {
        mainCamera = Camera.main;
        http = new HTTPManager(hostname);
    }

    public void HandleSubmitLogin(string username, string password)
    {
        Debug.Log($"Login -> \tEmail: \"{username}\"  \tPassword: \"{password}\"");
        string endpoint = "/login";
        WWWForm form = new WWWForm();
        form.AddField("email", username);
        form.AddField("password", password);

        StartCoroutine(http.PostRequest(endpoint, form, response =>
        {
            if (response != null)
            {
                // Handle POST response here
                token = JsonUtility.FromJson<HTTPResponse<string>>(response).data;
                OnAuthenticated.Invoke(token);
            }
            else
            {
                // Handle POST error here
            }
        }));
    }

    public void HandleSubmitLogout()
    {
        
        token = null;
        OnUnauthenticated.Invoke();
        /* string endpoint = "/logout";
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        StartCoroutine(http.PostRequest(endpoint, form, response =>
        {
            if (response != null)
            {
                // Handle POST response here
                Debug.Log(response);
            }
            else
            {
                // Handle POST error here
                Debug.LogError("Deu erro!");
            }
        })); */
    }

    public void GetUserHeroes ()
    {
        string endpoint = "/heroes";

        StartCoroutine(http.GetRequest(endpoint, token, response =>
        {
            if (response != null)
            {
                // Handle POST response here
                Debug.Log(response);
                heroes = JsonUtility.FromJson<HTTPResponse<UnitData[]>>(response).data;
                Debug.Log(heroes);
            }
            else
            {
                // Handle POST error here
            }
        }));
    }

    public void SendOrder (CharacterOrderData orderData)
    {
        OnSendOrder.Invoke(orderData);
    }
}
