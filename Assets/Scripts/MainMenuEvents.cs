using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _start_button;
    private Button _exit_button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
           
        _start_button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _exit_button = _document.rootVisualElement.Q("ExitGameButton") as Button;
        _start_button.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _exit_button.RegisterCallback<ClickEvent>(OnExitGameClick);
    }

    private void OnDisable()
    {
        _start_button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _exit_button.UnregisterCallback<ClickEvent>(OnExitGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Button");
    }

    private void OnExitGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Exit Button");
    }
}
