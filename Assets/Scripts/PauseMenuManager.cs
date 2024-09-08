using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public event Action OnRestartGame;

    [SerializeField]
    private Button _restart;
    [SerializeField]
    private Button _exit;
    [SerializeField]
    private Button _closePanel;

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _exit.onClick.AddListener(ExitGame);
        _closePanel.onClick.AddListener(ClosePanel);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        OnRestartGame?.Invoke();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}