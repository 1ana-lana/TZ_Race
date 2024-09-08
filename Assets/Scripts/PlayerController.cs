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
    private BonusEffect _shieldEffect;

    [SerializeField]
    private float roadLeftBoundary = -5f;
    [SerializeField]
    private float roadRightBoundary = 5f;

    private float _turnSpeed = 20;
    private float _decelerationRate = 5f;
    private float _minSpeed = 0.1f;
    private float horizontalInput = 0;


    private Coroutine _decelerationRoutine;
    [SerializeField]
    private float _currentSpeed;

    public float CurrentSpeed { get { return _currentSpeed; } }

    public void TapGas(bool state)
    {
        if (state)
        {
            if (_decelerationRoutine != null)
            {
                StopCoroutine(_decelerationRoutine);
            }

            if (_nitroEffect.IsBonusActive)
            {
                _currentSpeed = _nitroEffect.NitroSpeed;
            }
            else
            {
                _currentSpeed = BaseSpeed;
            }
        }
        else
        {
            if (_decelerationRoutine != null)
            {
                StopCoroutine(_decelerationRoutine);
            }

            _decelerationRoutine = StartCoroutine(Brake());
        }
    }

    public void TapBrake()
    {
        _currentSpeed = 0;
    }

    public void TouchСarSteeringWheel(bool touch, Vector2 touchPosition)
    {
        if (!touch)
        {
            horizontalInput = 0;
            return;
        }

        if (touchPosition.x < 0)
        {
            horizontalInput = -1;

        }
        else
        {
            horizontalInput = 1;

        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            _currentSpeed = BaseSpeed;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            _currentSpeed = 0;
        }
#endif

        Move();
    }

    private IEnumerator Brake()
    {
        while (_currentSpeed > 0)
        {
            _currentSpeed -= _decelerationRate * Time.deltaTime;
            if (_currentSpeed < _minSpeed)
            {
                _currentSpeed = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _currentSpeed);

#if UNITY_EDITOR
        float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
#endif
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
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }
        }

        if (collision.gameObject.CompareTag(ObstacleType.RoadCrack.ToString()))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }
        }

        if (collision.gameObject.CompareTag(ObstacleType.Block.ToString()))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }
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
            _nitroEffect.ActivateBonus(() => _currentSpeed = _nitroEffect.NitroSpeed,
                () =>
                {
                    if (_currentSpeed != 0)
                    {
                        _currentSpeed = BaseSpeed;
                    }
                }
           );
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            _shieldEffect.ActivateBonus(() => { }, () => { });
            Destroy(collision.gameObject);
        }
    }
}
