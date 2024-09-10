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
    [SerializeField]
    private FillImageController _healthFilledImage;
    [SerializeField]
    private FillImageController _shieldFilledImage;
    [SerializeField]
    private FillImageController _magnetFilledImage;
    [SerializeField]
    private FillImageController _nitroFilledImage;

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

    public void ActiveGameOverMenu(int currentScore)
    {
        _gameOverMenuManager.SetScore(currentScore);
        _gameOverMenuManager.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ActiveBonusBar(CollisionObjectType type)
    {
        switch (type)
        {
            case CollisionObjectType.Shield:
                _shieldFilledImage.gameObject.SetActive(true);
                _shieldFilledImage.SetNewState(0);
                break;
            case CollisionObjectType.Magnet:
                _magnetFilledImage.gameObject.SetActive(true);
                _magnetFilledImage.SetNewState(0);
                break;
            case CollisionObjectType.Nitro:
                _nitroFilledImage.gameObject.SetActive(true);
                _nitroFilledImage.SetNewState(0);
                break;
        }
    }

    public void ChangeHealthBar(float currentPoints)
    {
        _healthFilledImage.SetNewState(currentPoints);
    }

    private void ActivePauseMenu()
    {
        _pauseMenuPanel.gameObject.SetActive(true);
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
