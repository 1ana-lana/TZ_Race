using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _backTranform;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _emptyRoad;

    private float _backSize;
    private float _backPosition;
    private float traveledDistance;
    private bool _changedSprite = false;

    void Start()
    {
        _backSize = _spriteRenderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _speed = -5;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _speed = 0;
        }

        Move();
    }

    private void Move()
    {
        var step = _speed * Time.deltaTime;
        _backPosition += step;
        traveledDistance += step;
        Debug.Log("traveledDistance_" + traveledDistance);

        if (!_changedSprite && -traveledDistance > _backSize / 10)
        {
            ChangeSprite();
        }

        _backPosition = Mathf.Repeat(_backPosition, _backSize);
        _backTranform.position = new Vector3(0, _backPosition, 0);
    }

    private void ChangeSprite()
    {
        _changedSprite = true;
        _spriteRenderer.sprite = _emptyRoad;
    }
}
