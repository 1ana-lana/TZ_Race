using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _obstacles;
    [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private float _roadLeftBoundary = -3.5f;
    [SerializeField]
    private float _roadRightBoundary = 3.5f;

    private float _spawnDistance = 20f;
    private float _intervalBetweenSpawn = 1f;
    private float _obstacleLifetime = 10f; 
    private float _nextSpawnTime;

    void Start()
    {
        _nextSpawnTime = Time.time + _intervalBetweenSpawn;
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime && _player.Speed != 0)
        {
            SpawnObstacle();
            _nextSpawnTime = Time.time + _intervalBetweenSpawn;
        }
    }

    void SpawnObstacle()
    {
        int obstacleNumber = Random.Range(0, _obstacles.Count);
        GameObject obstaclePrefab = _obstacles[obstacleNumber];

        Vector3 spawnPosition = new Vector3(0, _player.transform.position.y + _spawnDistance, 0);
        //spawnPosition.x = Random.Range(_roadLeftBoundary, _roadRightBoundary);

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Destroy(newObstacle, _obstacleLifetime); 
    }
}
