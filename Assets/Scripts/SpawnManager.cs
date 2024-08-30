using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private RoadSpawner _roadSpawner;

    public void TriggerEnteredSpawn()
    {
        _roadSpawner.MoveRoad();
    }
}
