using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public event Action OnSectionTriggerEntered;
    public event Action OnGameOver;

    private const string TriggerTag = "SectionTrigger";
    private const float BaseSpeed = 20f;

    [SerializeField]
    private MagnetEffect _magnetEffect;
    [SerializeField]
    private NitroEffect _nitroEffect;
    [SerializeField]
    private float roadLeftBoundary = -5f;
    [SerializeField]
    private float roadRightBoundary = 5f;

    private float _turnSpeed = 20;
    private float _decelerationRate = 5f;
    private float _minSpeed = 0.1f;
    private Coroutine _decelerationRoutine;

    [SerializeField]
    private float _currentSpeed;
    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        private set { _currentSpeed = value; }
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            if (_decelerationRoutine != null)
            {
                StopCoroutine(_decelerationRoutine);
                Debug.LogError("StopCoroutine");
            }

            CurrentSpeed = BaseSpeed;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            if (_decelerationRoutine != null)
            {
                StopCoroutine(_decelerationRoutine);
                Debug.LogError("StopCoroutine");
            }

            _decelerationRoutine = StartCoroutine(Brake());
        }

        if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            CurrentSpeed = 0;
        }

        Move();
    }

    private IEnumerator Brake()
    {
        while (CurrentSpeed > 0)
        {
            CurrentSpeed -= _decelerationRate * Time.deltaTime;
            if (CurrentSpeed < _minSpeed)
            {
                CurrentSpeed = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * CurrentSpeed);

        float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
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
            OnGameOver?.Invoke();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Magnet"))
        {
            _magnetEffect.ActivateBonus(() => { }, () => { });
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("HP"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Nitro"))
        {
            Destroy(collision.gameObject);
            _nitroEffect.ActivateBonus(() => CurrentSpeed = _nitroEffect.NitroSpeed,
                ()=> CurrentSpeed = BaseSpeed);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
        }
    }
}
