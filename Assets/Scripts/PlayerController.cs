using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnSectionTriggerEntered;

    private const string TriggerTag = "SectionTrigger";

    [SerializeField]
    private float roadLeftBoundary = -5f;
    [SerializeField]
    private float roadRightBoundary = 5f;

    private float _speed = 0;
    private float _turnSpeed = 20;

    public float Speed { get => _speed; private set { _speed = value; } }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _speed = 20;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _speed = 0;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
        
        float horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontalInput * _turnSpeed * Time.deltaTime);
        Vector3 newPosition = transform.position + Vector3.right * horizontalInput * _turnSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, roadLeftBoundary, roadRightBoundary);

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TriggerTag))
        {
            OnSectionTriggerEntered?.Invoke();
        }
    }
}
