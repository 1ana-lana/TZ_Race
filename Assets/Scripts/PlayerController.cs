using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnSectionTriggerEntered;
    public event Action OnGameOver;

    private const string TriggerTag = "SectionTrigger";

    [SerializeField]
    private float roadLeftBoundary = -5f;
    [SerializeField]
    private float roadRightBoundary = 5f;
    [SerializeField]
    private MagnetEffect _magnetEffect;

    private float _turnSpeed = 20;

    public float Speed { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Speed = 20;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Speed = 0;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        
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

        if (collision.gameObject.CompareTag(ObstacleType.ObstacleOil.ToString()))
        {
            Debug.Log(ObstacleType.ObstacleOil.ToString());
        }

        if (collision.gameObject.CompareTag(ObstacleType.RoadCrack.ToString()))
        {
            Debug.Log(ObstacleType.RoadCrack.ToString());
        }

        if (collision.gameObject.CompareTag(ObstacleType.Block.ToString()))
        {
            Debug.Log(ObstacleType.Block.ToString());
            OnGameOver?.Invoke();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Magnet"))
        {
            Debug.Log("Magnet");
            _magnetEffect.ActivateMagnet();
            Destroy(collision.gameObject);
        }
    }
}
