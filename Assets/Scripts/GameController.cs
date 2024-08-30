using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController.OnSectionTriggerEntered += _playerController_OnTriggerEntered;
    }

    private void _playerController_OnTriggerEntered()
    {
        _spawnManager.TriggerEnteredSpawn();
    }
}
