using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private PlayerController _playerController; 
    [SerializeField]
    private UIController _uiController;

    private int _score;

    private void Awake()
    {
        _playerController.OnGetCoin += PlayerController_OnGetCoin;
        _playerController.OnHealthChanged += PlayerController_OnHealthChanged;
        _playerController.OnGameOver += PlayerController_OnGameOver;
        _playerController.OnSectionTriggerEntered += _playerController_OnTriggerEntered;
        
        _uiController.OnTapBrake += UiController_OnTapBrake;
        _uiController.OnTapGas += UiController_OnTapGas;
        _uiController.OnTouchСarSteeringWheel += UiController_OnTouchСarSteeringWheel;
    }

    private void PlayerController_OnHealthChanged(float health)
    {
        _uiController.ChangeHealthBar(health);
    }

    private void PlayerController_OnGetCoin()
    {
        _score++;
        _uiController.UpdateScore(_score);
    }

    private void PlayerController_OnGameOver()
    {
        _uiController.ActiveGameOverMenu(_score);
    }

    private void UiController_OnTouchСarSteeringWheel(bool touch, Vector2 touchPosition)
    {
        _playerController.TouchСarSteeringWheel(touch, touchPosition);
    }

    private void UiController_OnTapGas(bool state)
    {
        _playerController.TapGas(state);
    }

    private void UiController_OnTapBrake(bool state)
    {
        _playerController.TapBrake();
    }

    private void _playerController_OnTriggerEntered()
    {
        _spawnManager.TriggerEnteredSpawn();
    }
}
