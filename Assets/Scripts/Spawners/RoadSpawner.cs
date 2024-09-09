using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _roads;
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

        GameObject moveRoad = _roads[0];
        _roads.Remove(moveRoad);
        float newY= _roads[_roads.Count - 1].transform.position.y + _offset;
        moveRoad.transform.position = new Vector3(0, newY, 0);
        _roads.Add(moveRoad);
    }

    private void Start()
    {
        if (_roads != null && _roads.Count > 0)
        {
            _roads = _roads.OrderBy(r => r.transform.position.y ).ToList();
        }
        else
        {
            Debug.LogError("list of road parts is empty");
        }
    }

}
