using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnSectionTriggerEntered;

    private const string TriggerTag = "SectionTrigger";

    private float _speed = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _speed = -20;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _speed = 0;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TriggerTag))
        {
            OnSectionTriggerEntered?.Invoke();
        }
    }
}
