using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _volume_button;
    private bool flag;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _volume_button = _document.rootVisualElement.Q("SoundSetting") as Button;
        flag = true;
    }

    private void OnDisable()
    {
        _volume_button.UnregisterCallback<ClickEvent>(OnVolumeClick);
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
