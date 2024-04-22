using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracking : MonoBehaviour
{
    [SerializeField] private Text ScoreText;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ((Time.time - startTime) * 100f).ToString("F0");

        if (GameOver())
        {
            string finalScore = ScoreText.text;
            Debug.Log("You are dead! Your score is: "+finalScore);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameEnd(int.Parse(finalScore));
        }

    }

    bool GameOver()
    {
        return transform.position.y < -6f;
    }

}
