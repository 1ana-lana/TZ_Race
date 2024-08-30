using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> roads;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _emptyRoad;

    private float _offset = 30f;
    private bool _firstSpawn = true;

    public void MoveRoad()
    {
        if (_firstSpawn)
        {
            _firstSpawn = false;
            _spriteRenderer.sprite = _emptyRoad;
        }

        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);
        float newY= roads[roads.Count - 1].transform.position.y + _offset;
        moveRoad.transform.position = new Vector3(0, newY, 0);
        roads.Add(moveRoad);
    }

    private void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.y ).ToList();
        }   
    }

}
