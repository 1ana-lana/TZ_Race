using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

public enum CollisionObjectType
{
    ObstacleOil = 0,
    RoadCrack = 1,
    Block = 2,
    Shield = 3,
    Magnet = 4,
    Nitro = 5
}
