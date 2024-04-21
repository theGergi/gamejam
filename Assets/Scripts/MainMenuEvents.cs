using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _start_button;
    private Button _exit_button;
    private Button _volume_button;
    private Button _leaderboard_button;

    private VisualElement _leaderboard_menu;
    private VisualElement _main_menu;

    private ListView _leaderboard;

    private bool flag;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
           
        _start_button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _exit_button = _document.rootVisualElement.Q("ExitGameButton") as Button;
        _leaderboard_button = _document.rootVisualElement.Q("LeaderboardButton") as Button;



        _start_button.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _exit_button.RegisterCallback<ClickEvent>(OnExitGameClick);
        _leaderboard_button.RegisterCallback<ClickEvent>(OnLeaderboardClick);

        _volume_button = _document.rootVisualElement.Q("SoundSetting") as Button;
        flag = true;
        _volume_button.RegisterCallback<ClickEvent>(OnVolumeClick);

        _main_menu = _document.rootVisualElement.Q("MainMenu") as VisualElement;
        _leaderboard_menu = _document.rootVisualElement.Q("Leaderboard") as VisualElement;
        _leaderboard = _document.rootVisualElement.Q("Scores") as ListView;


        _main_menu.style.display = DisplayStyle.Flex;
        _leaderboard_menu.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        _start_button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _exit_button.UnregisterCallback<ClickEvent>(OnExitGameClick);
        _volume_button.UnregisterCallback<ClickEvent>(OnVolumeClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Button");
    }

    private void OnExitGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Exit Button");
    }

    private void OnLeaderboardClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Leaderboard Button");


        _main_menu.style.display = DisplayStyle.None;
        _leaderboard_menu.style.display = DisplayStyle.Flex;
        //_leaderboard_menu.SetEnabled(true);
        //_main_menu.SetEnabled(false);


        GameObject.Find("GameManager").GetComponent<GameManager>().updateLeaderboard(_leaderboard);
    }

    private void OnVolumeClick(ClickEvent evt)
    {
        if (flag)
        {
            Debug.Log("You muted the game");
            flag = false;
        }
        else
        {
            Debug.Log("You unmuted the game");
            flag = true;
        }
    }
}
