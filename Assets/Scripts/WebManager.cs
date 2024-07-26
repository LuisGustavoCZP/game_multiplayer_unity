using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void JsFunction(string str);

    public void ReceiveData(string data)
    {
        Debug.Log("Received data from JavaScript: " + data);
        // Faça algo com os dados recebidos
    }

    public void SendDataToJS(string data)
    {
        JsFunction(data);
    }
}