using System.Collections;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField]
    private float _magnetRadius = 5f; 
    [SerializeField] 
    private float _attractionSpeed = 10f;
    [SerializeField]
    private float _activeTime = 15f;
    [SerializeField]
    private LayerMask _coinLayer;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private bool isMagnetActive = false;

    void Update()
    {
        if (isMagnetActive)
        {
            AttractCoins();
        }
    }

    public void ActivateMagnet()
    {
        isMagnetActive = true;
        _spriteRenderer.enabled = true;
        StartCoroutine(DeactivateMagnet());
    }

    private IEnumerator DeactivateMagnet()
    {
        yield return new WaitForSeconds(_activeTime);
        isMagnetActive = false;
        _spriteRenderer.enabled = false;
    }

    void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, _magnetRadius, _coinLayer);
        Debug.Log(coins.Length);
        foreach (Collider2D coinCollider in coins)
        {
            Transform coinTransform = coinCollider.transform;
            Vector3 direction = (transform.position - coinTransform.position).normalized;
            coinTransform.position += direction * _attractionSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, coinTransform.position) < 0.5f)
            {
                CollectCoin(coinCollider.gameObject);
            }
        }
    }

    void CollectCoin(GameObject coin)
    {
        Destroy(coin);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius); // magnet visual
    }
}
