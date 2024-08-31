using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _obstacles;
    [SerializeField]
    private List<GameObject> _bonuses;
   [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private float _roadLeftBoundary = -3.5f;
    [SerializeField]
    private float _roadRightBoundary = 3.5f;

    private float _spawnDistance = 20f;
    private float _intervalBetweenSpawn = 1f;
    private float _nextSpawnTime;

    private int spawnBonusePercent = 20;

    void Start()
    {
        _nextSpawnTime = Time.time + _intervalBetweenSpawn;
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime && _player.Speed != 0)
        {
            ChooseSpawnObject();
            _nextSpawnTime = Time.time + _intervalBetweenSpawn;
        }
    }
    private void ChooseSpawnObject()
    {
        int spawnChance = Random.Range(0, 100);
        if (spawnChance > spawnBonusePercent)
        {
            SpawnObject(_obstacles);
        }
        else
        {
            SpawnObject(_bonuses);
        }
    }

    private void SpawnObject(List<GameObject> toSpawn)
    {
        int objectNumber = Random.Range(0, toSpawn.Count);
        GameObject objectPrefab = toSpawn[objectNumber];

        Vector3 spawnPosition = new Vector3(0, _player.transform.position.y + _spawnDistance, 0);
        //spawnPosition.x = Random.Range(_roadLeftBoundary, _roadRightBoundary);

        GameObject newObstacle = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }
}
