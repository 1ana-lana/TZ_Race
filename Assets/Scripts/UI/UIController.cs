using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public event Action<bool> OnTapGas;
    public event Action<bool> OnTapBrake;
    public event Action<bool, Vector2> OnTouchСarSteeringWheel;

    [SerializeField]
    private PedalButton _gasPedalButton;
    [SerializeField] 
    private PedalButton _brakePedalButton;
    [SerializeField]
    private СarSteeringWheel _сarSteeringWheel;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private PauseMenuManager _pauseMenuPanel;
    [SerializeField]
    private GameOverMenuManager _gameOverMenuManager;
    [SerializeField]
    private TMP_Text _currentScore;

    private void Awake()
    {
        _gasPedalButton.OnTouch += GasPedalButton_OnTouch;
        _brakePedalButton.OnTouch += BrakePedalButton_OnTouch;
        _сarSteeringWheel.OnTouch += CarSteeringWheel_OnTouch;
        _pauseButton.onClick.AddListener(ActivePauseMenu);
    }

    public void UpdateScore(int score)
    {
        _currentScore.text = "Score:" + Environment.NewLine + score;
    }

    private void ActivePauseMenu()
    {
        _pauseMenuPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ActiveGameOverMenu(int currentScore)
    {
        _gameOverMenuManager.SetScore(currentScore);
        _gameOverMenuManager.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void CarSteeringWheel_OnTouch(bool touch, Vector2 touchPosition)
    {
        OnTouchСarSteeringWheel?.Invoke(touch, touchPosition);
    }

    private void BrakePedalButton_OnTouch(bool state)
    {
        OnTapBrake?.Invoke(state);
    }

    private void GasPedalButton_OnTouch(bool state)
    {
        OnTapGas?.Invoke(state);
    }
}
