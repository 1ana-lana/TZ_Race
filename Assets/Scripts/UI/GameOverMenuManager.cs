using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button _restart;
    [SerializeField]
    private Button _exit;
    [SerializeField]
    private TMP_Text _currentScore;
    [SerializeField]
    private TMP_Text _bestScore;

    private HighScoreManager _highScoreManager = new HighScoreManager();

    public void SetScore(int score)
    {
        int bestScore = _highScoreManager.Load();
        _currentScore.text = "Your Score:" + Environment.NewLine + score;
        _bestScore.text = "Best Score:" + Environment.NewLine + bestScore;

        _highScoreManager.Save(score);
    }

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _exit.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.name);
    }
}
