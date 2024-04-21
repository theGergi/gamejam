using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _start_button;
    private Button _exit_button;
    private Button _volume_button;
    private Button _leaderboard_button;
    private Button _exit_leaderboard_button;
    private Button _ready_button;

    private VisualElement _leaderboard_menu;
    private VisualElement _main_menu;
    private VisualElement _name_menu;

    private Label _score_label;

    private ListView _leaderboard;

    private TextField _name_field;

    private bool flag;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _ready_button = _document.rootVisualElement.Q("ReadyButton") as Button;
        _ready_button.RegisterCallback<ClickEvent>(OnReadyClick);

        _start_button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _start_button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _exit_button = _document.rootVisualElement.Q("ExitButton") as Button;
        _exit_button.RegisterCallback<ClickEvent>(OnExitGameClick);

        _leaderboard_button = _document.rootVisualElement.Q("LeaderboardButton") as Button;
        _leaderboard_button.RegisterCallback<ClickEvent>(OnLeaderboardClick);

        flag = true;
        _volume_button = _document.rootVisualElement.Q("SoundSetting") as Button;
        _volume_button.RegisterCallback<ClickEvent>(OnVolumeClick);

        _main_menu = _document.rootVisualElement.Q("MainMenu") as VisualElement;
        _leaderboard_menu = _document.rootVisualElement.Q("Leaderboard") as VisualElement;
        _name_menu = _document.rootVisualElement.Q("NameMenu") as VisualElement;

        _leaderboard = _document.rootVisualElement.Q("Scores") as ListView;

        _exit_leaderboard_button = _document.rootVisualElement.Q("ExitLeaderboardButton") as Button;
        _exit_leaderboard_button.RegisterCallback<ClickEvent>(OnExitLeaderboardClick);

        _score_label = _document.rootVisualElement.Q("Score") as Label;

        _name_field = _document.rootVisualElement.Q("NameField") as TextField;

        if (GameObject.Find("GameManager").GetComponent<GameManager>().isGameEnd) {
            _main_menu.style.display = DisplayStyle.None;
            _leaderboard_menu.style.display = DisplayStyle.Flex;
            _name_menu.style.display = DisplayStyle.None;
            GameObject.Find("GameManager").GetComponent<GameManager>().isGameEnd = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().updateLeaderboard(_leaderboard);
            GameObject.Find("GameManager").GetComponent<GameManager>().getOwnScore(_score_label);
        }
        else {
            _main_menu.style.display = DisplayStyle.None;
            _leaderboard_menu.style.display = DisplayStyle.None;
            _name_menu.style.display = DisplayStyle.Flex;
        }
    }

    private void OnDisable()
    {
        _start_button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _exit_button.UnregisterCallback<ClickEvent>(OnExitGameClick);
        _volume_button.UnregisterCallback<ClickEvent>(OnVolumeClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    private void OnExitGameClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnExitLeaderboardClick(ClickEvent evt)
    {
        _main_menu.style.display = DisplayStyle.Flex;
        _leaderboard_menu.style.display = DisplayStyle.None;
    }

    private void OnReadyClick(ClickEvent evt)
    {
        _main_menu.style.display = DisplayStyle.Flex;
        _leaderboard_menu.style.display = DisplayStyle.None;
        _name_menu.style.display = DisplayStyle.None;
        GameObject.Find("GameManager").GetComponent<GameManager>().playerName = _name_field.value;
    }

    private void OnLeaderboardClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Leaderboard Button");

        

        _main_menu.style.display = DisplayStyle.None;
        _leaderboard_menu.style.display = DisplayStyle.Flex;
        //_leaderboard_menu.SetEnabled(true);
        //_main_menu.SetEnabled(false);


        GameObject.Find("GameManager").GetComponent<GameManager>().updateLeaderboard(_leaderboard);
        GameObject.Find("GameManager").GetComponent<GameManager>().getOwnScore(_score_label);
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
