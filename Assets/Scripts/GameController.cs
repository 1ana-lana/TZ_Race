using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private PlayerController _playerController; 
    [SerializeField]
    private UIController _uiController;

    private void Awake()
    {
        _playerController.OnSectionTriggerEntered += _playerController_OnTriggerEntered;
        _uiController.OnTapBrake += UiController_OnTapBrake;
        _uiController.OnTapGas += UiController_OnTapGas;
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
