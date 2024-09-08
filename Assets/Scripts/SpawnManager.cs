using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private RoadSpawner _roadSpawner;
    [SerializeField]
    private ObjectSpawner _objectSpawner;
    [SerializeField]
    private BotSpawner _botSpawner;
    [SerializeField]
    private PlayerController _player;

    private void Awake()
    {
        _objectSpawner.Initialize(_player);
        _botSpawner.Initialize(_player.transform);
    }

    public void TriggerEnteredSpawn()
    {
        _roadSpawner.MoveRoad();
    }
}
