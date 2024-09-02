using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

public enum ObstacleType
{
    ObstacleOil = 0,
    RoadCrack = 1,
    Block = 2
}
