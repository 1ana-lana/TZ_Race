﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _obstacles;
    [SerializeField]
    private List<GameObject> _bonuses;
    [SerializeField]
    private float _roadLeftBoundary = -3.5f;
    [SerializeField]
    private float _roadRightBoundary = 3.5f;

    private PlayerController _player;

    private float _spawnDistance = 20f;
    private float _distanceBetweenSpawns = 15f;
    private float _lastSpawnPosition;

    private int _spawnBonusePercent = 30;

    public void Initialize(PlayerController player)
    {
        _player = player;
    }

    void Start()
    {
        _lastSpawnPosition = _player.transform.position.y;
    }

    void Update()
    {
        if (_player != null && _player.transform.position.y > _lastSpawnPosition + _distanceBetweenSpawns)
        {
            Vector3 spawnPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + _spawnDistance, _player.transform.position.z);

            ChooseSpawnObject(spawnPosition);

            _lastSpawnPosition = _player.transform.position.y;
        }
    }

    private void ChooseSpawnObject(Vector3 spawnPosition)
    {
        int spawnChance = Random.Range(0, 100);
        if (spawnChance > _spawnBonusePercent)
        {
            SpawnObject(_obstacles, spawnPosition);
        }
        else
        {
            SpawnObject(_bonuses, spawnPosition);
        }
    }

    private void SpawnObject(List<GameObject> toSpawn, Vector3 spawnPosition)
    {
        int objectNumber = Random.Range(0, toSpawn.Count);
        GameObject objectPrefab = toSpawn[objectNumber];

        spawnPosition.x = Random.Range(_roadLeftBoundary, _roadRightBoundary);

        GameObject newObstacle = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }
}
