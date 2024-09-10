using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public event Action OnGetCoin;
    public event Action<float> OnHealthChanged;
    public event Action OnSectionTriggerEntered;
    public event Action OnGameOver;
    public event Action<CollisionObjectType> OnBonusOn;

    private const string TriggerTag = "SectionTrigger";
    private const float BaseSpeed = 20f;
    private const float FullHealthPoints = 1f;
    private const float HeartHealthPoints = 0.25f;

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

    [SerializeField]
    private GameObject enemyCollisionImagePrefab;

    private float _turnSpeed = 20;
    private float _decelerationRate = 5f;
    private float _minSpeed = 0.1f;
    private float _horizontalInput = 0;
    private float _currentSpeed;
    private float _health = 1f;

    private Coroutine _decelerationRoutine;
    private Coroutine _slowdownRoutine;

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
            _horizontalInput = 0;
            return;
        }

        if (touchPosition.x < 0)
        {
            _horizontalInput = -1;

        }
        else
        {
            _horizontalInput = 1;

        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentSpeed = BaseSpeed;
        }

        if (UnityEngine.Input.GetKeyUp(KeyCode.UpArrow))
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

    private void ChangeHealth(float healthPoint)
    {
        _health += healthPoint;
        if (_health <= 0)
        {
            _health = 0;
            OnGameOver?.Invoke();
        }

        if (_health > FullHealthPoints)
        {
            _health = FullHealthPoints;
        }

        OnHealthChanged?.Invoke(_health);
    }

    private void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _currentSpeed);

#if UNITY_EDITOR
        _horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
#endif
        Vector3 newPosition = transform.position + Vector3.right * _horizontalInput * _turnSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, roadLeftBoundary, roadRightBoundary);

        transform.position = newPosition;
    }

    private IEnumerator Slowdown()
    {
        _currentSpeed = BaseSpeed / 2;
        yield return new WaitForSeconds(10f);
        _currentSpeed = BaseSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TriggerTag))
        {
            OnSectionTriggerEntered?.Invoke();
        }

        if (collision.gameObject.CompareTag(CollisionObjectType.ObstacleOil.ToString()))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }

            ActiveSlowdown();
        }

        if (collision.gameObject.CompareTag(CollisionObjectType.RoadCrack.ToString()))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }

            ChangeHealth(-HeartHealthPoints);
            ActiveSlowdown();
        }

        if (collision.gameObject.CompareTag(CollisionObjectType.Block.ToString()))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }
            OnGameOver?.Invoke();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_shieldEffect.IsBonusActive)
            {
                return;
            }

            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            Instantiate(enemyCollisionImagePrefab, contactPoint, Quaternion.identity);

            OnGameOver?.Invoke();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            OnGetCoin?.Invoke();
        }

        if (collision.gameObject.CompareTag("HP"))
        {
            ChangeHealth(HeartHealthPoints);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Magnet"))
        {
            _magnetEffect.ActivateBonus(() => { }, () => { });
            Destroy(collision.gameObject);
            OnBonusOn?.Invoke(CollisionObjectType.Magnet);
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
            OnBonusOn?.Invoke(CollisionObjectType.Nitro);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            _shieldEffect.ActivateBonus(() => { }, () => { });
            Destroy(collision.gameObject);
            OnBonusOn?.Invoke(CollisionObjectType.Shield);
        }
    }

    private void ActiveSlowdown()
    {
        if (_slowdownRoutine != null)
        {
            StopCoroutine(_slowdownRoutine);
        }

        _slowdownRoutine = StartCoroutine(Slowdown());
    }
}
