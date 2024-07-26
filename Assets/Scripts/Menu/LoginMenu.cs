using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LoginMenu : MonoBehaviour
{
    public UnityEvent<string, string> OnSubmit;

    UIDocument ui = null;
    VisualElement root;

    TextField usernameField;
    TextField passwordField;
    Button loginButton;

    void Awake()
    {
        ui = GetComponent<UIDocument>();
    }
    
    void OnEnable()
    {
        root = ui.rootVisualElement;
        loginButton = root.Q<Button>("login_button");
        usernameField = root.Q<TextField>("username_field");
        passwordField = root.Q<TextField>("password_field");
        loginButton.RegisterCallback<ClickEvent>(HandleSubmit);
    }

    void HandleSubmit (ClickEvent evt)
    {
        OnSubmit.Invoke(usernameField.value, passwordField.value);
    }
}
