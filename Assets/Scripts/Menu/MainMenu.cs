using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UnityEvent<MatchData> OnSkimishStart;
    public UnityEvent OnLogout;

    UIDocument ui = null;
    VisualElement root;
    
    VisualElement mainContainer = null;
    Button logoutButton;
    Button skimishButton;

    public MatchData matchData;

    void Awake()
    {
        ui = GetComponent<UIDocument>();
    }
    
    void OnEnable()
    {
        root = ui.rootVisualElement;
        mainContainer = root.Q<Button>("main_container");
        skimishButton = root.Q<Button>("skimish_button");
        logoutButton = root.Q<Button>("logout_button");
    
        skimishButton.RegisterCallback<ClickEvent>(HandleSkimish);
        logoutButton.RegisterCallback<ClickEvent>(HandleLogout);
    }

    void HandleLogout (ClickEvent evt)
    {
        OnLogout.Invoke();
    }

    void HandleSkimish (ClickEvent evt)
    {
        OnSkimishStart.Invoke(matchData);
    }
}
