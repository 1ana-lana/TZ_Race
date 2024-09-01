using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Debug.Log("Object became invisible_"+gameObject.tag);
        Destroy(gameObject);
    }
}

public enum ObstacleType
{
    ObstacleOil = 0,
    RoadCrack = 1,
    Block = 2
}
