using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public event Action OnBecameInvisible;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _stopDistance = 4f;
    [SerializeField]
    private float _obstacleAvoidanceDistance = 5f;
    [SerializeField]
    private float _avoidSpeed = 2f;
    [SerializeField]
    private LayerMask _obstacleLayer;
    [SerializeField]
    private Collider2D _objectCollider;

    private Transform _player;
    private bool _wasVisible = false;

    public void Initialize(Transform player)
    {
        _player = player;
    }

    private void Update()
    {
        CheckVisible();

        CheckDistance();
    }

    private bool IsObjectInCameraView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, _objectCollider.bounds);
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance > _stopDistance)
        {
            Vector2 obstaclePosition;
            if (IsObstacleAhead(out obstaclePosition))
            {
                AvoidObstacle(obstaclePosition);
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (_player.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private bool IsObstacleAhead(out Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _obstacleAvoidanceDistance, _obstacleLayer);

        if (hit.collider != null)
        {
            position = hit.collider.transform.position;
        }
        else
        {
            position = new Vector2();
        }

        return hit.collider != null;
    }

    private void AvoidObstacle(Vector2 obstaclePosition)
    {
        Vector2 avoidDirection;
        if (obstaclePosition.x > 0)
        {
            avoidDirection = Vector2.left;
        }
        else
        {
            avoidDirection = Vector2.right;
        }

        transform.Translate(avoidDirection * _avoidSpeed * Time.deltaTime);
    }

    private void CheckVisible()
    {
        if (IsObjectInCameraView() && !_wasVisible)
        {
            _wasVisible = true;
        }
        else if (!IsObjectInCameraView() && _wasVisible)
        {
            _wasVisible = false;
            OnBecameInvisible?.Invoke();
            Destroy(gameObject);
        }
    }
}